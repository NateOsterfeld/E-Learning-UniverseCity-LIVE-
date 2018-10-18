/// <reference path="jquery-3.3.1.min.js" />
/// <reference path="jquery.validate.min.js" />

$(document).ready(function ()
{

    // Validate takes an object, not a function
    // Objects in javascript use { .. } notation and are the same as key / value pairs
    $("#registrationId").validate({

        debug: false,
        rules: {
            FirstName: {
                required: true,         //makes first name required
                minlength: 2,
            },
            LastName: {
                required: true,         //makes last name required
                minlength: 2,
            },
            Email: {
                required: true,
                email: true,
            },
            Password: {
                required: true,         //requires password field
                minlength: 8,           //requires at least 8 characters
                strongpassword: true    //uses custom validator for strong password
            },
            ConfirmPassword: {
                equalTo: "#Password"    //uses the CSS selector to match value of the field
            },
        },
        messages: {
            FirstName: {
                required: " First Name Required",
                minlength: " Must be at least 2 characters",
            },
            LastName: {
                required: " Last Name Required",
                minlength: " Must be at least 2 characters",
            },
            Email: {
                required: " Email Required",
                email: " Provide Valid Email Address"
            }
        },
        errorClass: "error mdc-text-field-helper-text--validation-msg",
        errorElement: "p",
        validClass: "valid"
    });

});

$(document).ready(function ()
{

    // Validate takes an object, not a function
    // Objects in javascript use { .. } notation and are the same as key / value pairs
    $("#loginId").validate({

        debug: false,
        rules: {
            Email: {
                required: true,
                email: true,
            },
            Password: {
                required: true,         //requires password field
                minlength: 8,           //requires at least 8 characters
                strongpassword: false    //uses custom validator for strong password // EVAN TURNED THIS OFF TEMP
            }
        },
        messages: {
            Email: {
                required: " Email Required",
                email: " Provide Valid Email Address"
            }            
        },
        errorClass: "error mdc-text-field-helper-text--validation-msg",
        errorElement: "p",
        validClass: "valid"
    });

});

$(document).ready(function ()
{

    $("#createCourseId").validate({

        debug: false,
        rules: {
            Name: {
                required: true,
                minlength: 2,
            },
            Description: {
                required: true,        
                minlength: 2,           
            },
            Cost: {
                required: true,
                min: 0                
            }
        },
        messages: {
            Name: {
                required: " Course Name Required",
                minLength: " Must contain at least 2 characters"
            },
            Description: {
                required: " Description Required",
                minLength: " Must contain at least 2 characters"
            },
            Cost: {
                required: " Cost Required",
                min: " Enter a number larger than 0"
            }
        },
        errorClass: "error mdc-text-field-helper-text--validation-msg",
        errorElement: "p",
        validClass: "valid"
    });

    $("#createAssignmentId").validate({

        debug: false,
        rules: {
            AssignmentName: {
                required: true,
                minlength: 2,
            },
            Instructions: {
                required: true,
                minlength: 2,
            }
        },
        messages: {
            AssignmentName: {
                required: " Assignment Name Required",
                minLength: " Must contain at least 2 characters"
            },
            Instructions: {
                required: " Instructions Required",
                minLength: " Must contain at least 2 characters"
            }
        },
        errorClass: "error mdc-text-field-helper-text--validation-msg",
        errorElement: "p",
        validClass: "valid"
    });
});



$.validator.addMethod("strongpassword", function (value, index)
{
    return value.match(/[A-Z]/) && value.match(/[a-z]/) && value.match(/\d/);  //check for one capital letter, one lower case letter, one num
}, "Please enter a stronger password (Must contain one capital, one lower case, and one number)");
