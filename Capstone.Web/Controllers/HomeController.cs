using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Capstone.Web
{
    public class HomeController : Controller
    {
        private const string RoleKey = "Role";
        IDatabaseDAL _db;
        private const string UserKey = "User";
        public const string CurrentCourseIdKey = "CurrentCourseId";

        public HomeController(IDatabaseDAL dal)
        {
            _db = dal;
        }

        public ActionResult DeleteAssignment(int assignmentId)
        {

            _db.DeleteAssignment(assignmentId);
            return RedirectToAction("DashboardTeacherAssignment", new { courseid = Session[CurrentCourseIdKey] });
        }

        // GET: Home
        public ActionResult Index()
        {
            ActionResult result = View();

            if (IsAuthenticated)
            {
                result = RedirectToAction("Dashboard","Home");
            }
            return result;
        }

        public ActionResult NotAuth()
        {
            return View();
        }


        public ActionResult DashboardAssignment(int courseId)
        {
            AssignmentDashBoardViewModel model = new AssignmentDashBoardViewModel()
            {
                Assignments = _db.GetAssignmentsForACourse(courseId),
                Course = _db.GetCourse(courseId),                
            };

            foreach (var assignment in model.Assignments)
            {
                if (assignment.Video == null)
                {
                    model.AssignmentsWithVideo = _db.GetAssignmentsWithVideoForACourse(courseId);
                }
            }

            model.User = _db.GetTeacherForCourse(model.Course.TeacherId);

            Session[CurrentCourseIdKey] = courseId;
            return View("DashboardStudentAssignment", model);
        }

        public ActionResult DashboardTeacherAssignment(int courseId)
        {
            AssignmentDashBoardViewModel model = new AssignmentDashBoardViewModel()
            {
                Assignments = _db.GetAssignmentsForACourse(courseId),
                Course = _db.GetCourse(courseId)
            };

            foreach(var assignment in model.Assignments)
            {
                if(assignment.Video == null)
                {
                    model.AssignmentsWithVideo = _db.GetAssignmentsWithVideoForACourse(courseId);
                }
            }

            model.User = _db.GetTeacherForCourse(model.Course.TeacherId);

            Session[CurrentCourseIdKey] = courseId;
            return View("DashboardTeacherAssignment", model);
        }


        public ActionResult DashboardTeacherStudentCourse(int courseId)
        {
            DashboardTeacherStudentCourseViewModel model = new DashboardTeacherStudentCourseViewModel()
            {
                Students = _db.GetUsersForCourse(courseId),
                Course = _db.GetCourse(courseId)
            };

            return View("DashboardTeacherStudentCourse", model);
        }


        public ActionResult _Enrollment()
        {
            ActionResult result = null;

            EnrollmentViewModel enrollModel = new EnrollmentViewModel
            {
                //gets all courses
                Courses = _db.GetCourses(),
                Users = _db.GetAllUsers()
            };

            if (IsStudent)
            {
                result = View("_Enrollment", enrollModel);
            }
            else // not logged in or not student
            {
                result = RedirectToAction("NotAuth", "Home");
            }

            return result;
        }

        public ActionResult SearchCourses(string search)
        {
            ActionResult result = null;

            EnrollmentViewModel enrollModel = new EnrollmentViewModel
            {
                Courses = _db.SearchCourses(search),
                Users = _db.GetAllUsers()
            };

            if (IsStudent)
            {
                result = View("_Enrollment", enrollModel);
            }
            else // not logged in or not student
            {
                result = RedirectToAction("NotAuth", "Home");
            }

            return result;
        }


        [HttpGet]
        [Route("Home/Register")]
        public ActionResult Register()
        {
            LogUserOut();
            var selListModel = ConvertListToSelectList(_db.GetRoles());
            return View("Register", selListModel);
        }

        [HttpPost]
        [Route("Home/Register")]
        public ActionResult Register(SignUpViewModel model)
        {
            ActionResult resultView = null;

            try
            {
                if (model.IsValid)
                {
                    var currentUser = _db.GetUser(model.Email);

                    if (currentUser != null)
                    {
                        ViewBag.ErrorMessage = "This username is unavailable";
                        throw new Exception();
                    }
                    else
                    {
                        PasswordHashHelper hash = new PasswordHashHelper(model.Password);

                        var newUser = new User
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            Password = model.Password,
                            HashedPassword = hash.Hash,
                            Salt = hash.Salt,
                            RoleId = model.RoleId
                        };

                        // Add user to database
                        newUser.UserId = _db.CreateUser(newUser);

                        // Log the user in and redirect to the dashboard
                        LogUserIn(newUser);

                        resultView = RedirectToAction("Dashboard", "Home");
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                var selListModel = ConvertListToSelectList(_db.GetRoles());
                resultView = View("Register", selListModel);
            }

            return resultView;
        }

        /// <summary>
        /// action that goes to the login page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Home/Login")]
        public ActionResult Login()
        {
            LogUserOut();

            var model = new LoginViewModel();
            return View("Login", model);
        }

        [HttpPost]
        [Route("Home/Login")]
        public ActionResult Login(LoginViewModel model)
        {
            ActionResult result = null;

            if (model.IsValid)
            { 

                var user = _db.GetUser(model.Email);
                var Hash = "";

                if (user != null)
                {
                    PasswordHashHelper hash = new PasswordHashHelper(model.Password, user.Salt);
                    Hash = hash.Hash;
                }

                if (user == null)
                {
                    ModelState.AddModelError("invalid-user", "The username provided does not exist");
                    return View("Login", model);
                }
                else if (user.Password != Hash)
                {
                    ModelState.AddModelError("invalid-password", "The password provided is not valid");
                    result = View("Login", model);
                }
                else
                {
                    // adds the user to the session variable using the username key
                    LogUserIn(user);

                    result = RedirectToAction("Dashboard", "Home");
                }
            }
            else
            { 
                result = View("Login", model);
            }

            return result;
        }

        [HttpGet]
        [Route("Logout")]
        public ActionResult Logout()
        {
            LogUserOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult _CreateCourse()
        {
            CreateCourseViewModel model = new CreateCourseViewModel();

            if (!IsTeacher)
            {
                RedirectToAction("NotAuth", "Home");

            }

            return View("_CreateCourse", model);
        }

        public ActionResult _CreateAssignment()
        {
            CreateAssignmentViewModel model = new CreateAssignmentViewModel();

            if (!IsTeacher)
            {
                RedirectToAction("NotAuth", "Home");
                
            }

            return View("_CreateAssignment", model);
        }

        public ActionResult _ViewStudentAssignment()
        {
            StudentAssignmentDashBoardViewModel model = new StudentAssignmentDashBoardViewModel();

            return View("_ViewStudentAssignment", model);
        }


        public ActionResult CourseDetail(int courseId)
        {
            var course = _db.GetCourse(courseId);

            return View("CourseDetail", course);
        }

        [HttpGet]
        [Route("Home/Dashboard")]
        public ActionResult Dashboard()
        {
            ActionResult result = null;

            if (!IsAuthenticated)
            {
                result = RedirectToAction("Login", "Home");
            }
            else
            {
              if (this.IsTeacher)
                {
                    DashboardTeacherViewModel dashboardModel = new DashboardTeacherViewModel
                    {
                        //Assignments = _db.GetStudentAssignmentsForTeacher(this.CurrentUser.UserId),
                        Courses = _db.GetCoursesForTeacher(this.CurrentUser.UserId),
                        Users = _db.GetUsersForTeacher(this.CurrentUser.UserId),
                        AllUsers = _db.GetAllUsers()
                    };

                    dashboardModel.CoursesWithPic = _db.GetCoursesWithPicForATeacher(this.CurrentUser.UserId);

                    result = View("DashboardTeacher", dashboardModel);
                }
                else
                {
                    DashboardStudentViewModel dashboardModel = new DashboardStudentViewModel
                    {
                        EnrolledCourses = _db.GetCoursesForStudent(this.CurrentUser.UserId),
                        Users = _db.GetAllUsers()
                    };

                    result = View("DashboardStudent", dashboardModel);
                }
            }
            return result;
        }


        /// <summary>
        /// returns true if a user is logged in. (user is storred in the session)
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                return Session[UserKey] != null;
            }
        }

        /// <summary>
        /// derived prop that returns true if the currently logged in user is a student.
        /// </summary>
        public bool IsStudent
        {
            get
            {
                bool result = false;
                if (IsAuthenticated)
                {
                    if (CurrentUser.RoleId == 1)
                    {
                        result = true;
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// derived prop that returns true if the currently logged in user is a teacher.
        /// </summary>
        public bool IsTeacher
        {
            get
            {
                bool result = false;
                if (IsAuthenticated)
                {
                    if (CurrentUser.RoleId == 2)
                    {
                        result = true;
                    }
                }
                return result;
            }
        }

        public User CurrentUser
        {
            get
            {
                User user = null;

                //Check to see if user cookie exists, if not create it
                if (Session[UserKey] != null)
                {
                    user = (User)Session[UserKey];
                }
                return user;
            }
        }

        public void LogUserOut()
        {
            Session.Abandon();
            //Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
        }

        public void LogUserIn(User user)
        {
            Session[UserKey] = user;
        }

        [HttpPost]
        public ActionResult NewCourse(CreateCourseViewModel model)
        {
            ActionResult result;

            //Validate the model before proceeding
            if (!ModelState.IsValid)
            {
                //not valid
                result = View("Dashboard", model);
            }
            else
            {
                //valid
                Course course = new Course
                {//-----------------------------NEEDS TO BE CHANGED
                    CourseName = model.Name,
                    Description = model.Description,
                    Difficulty = (int)model.DifficultyLevel,
                    TeacherId = CurrentUser.UserId,
                    CostUSD = model.Cost
                };

                course = _db.CreateCourse(course);
                course = _db.GetCourseId(course.CourseName);
                course.Image = _db.CreateFileForTeacherCourse(model.PostedFile, course.CourseId);

                _db.UpdateCourseWithFileId(course.CourseId);
                result = RedirectToAction("Index", "Home");
            }
            return result;
        }

        [HttpPost]
        public ActionResult NewAssignment(CreateAssignmentViewModel model, String courseId)
        {
            ActionResult result;
            model.CourseId = int.Parse(courseId);

            //Validate the model before proceeding
            if (!ModelState.IsValid)
            {
                result = View("Dashboard", model);
            }
            else
            {
                //valid
                Assignment assignment = new Assignment
                {
                    Instructions = model.Instructions,
                    AssignmentId = model.AssignmentId,
                    CourseId = model.CourseId,
                    AssignmentName = model.AssignmentName
                    
                };

                assignment = _db.CreateAssignment(assignment);
                assignment = _db.GetAssignmentId(assignment.AssignmentName);
                _db.CreateFileForTeacherAssignment(model.PostedFile, assignment.AssignmentId);
                _db.UpdateAssignmentWithFileId(assignment.AssignmentId);

                result = RedirectToAction("DashboardTeacherAssignment", new { courseId = int.Parse(courseId) });
            }
            return result;
        }

        private List<SelectListItem> ConvertListToSelectList(List<Role> roles)
        {
            List<SelectListItem> dropdownlist = new List<SelectListItem>();

            foreach (Role role in roles)
            {
                SelectListItem choice = new SelectListItem() { Text = role.RoleName, Value = Convert.ToString(role.RoleId) };
                dropdownlist.Add(choice);
            }

            return dropdownlist;
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    file.SaveAs(_path);
                }
            }
            catch
            {
                
            }
            return View("DashboardAssignment");
        }

        public ActionResult addCourseToStudent(int courseId)
        {
            _db.EnrollStudentInCourse(this.CurrentUser.UserId, courseId);

            return RedirectToAction("Dashboard");
        }

    }
}