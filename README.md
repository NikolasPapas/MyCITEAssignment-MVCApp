Part 2 – Web Application

The web application is to be implemented via the MVC framework relevant to the technology selected for the implementation 
of the application. The restrictions of the implementation are as follows:
- The application must use the MVC framework and should operate mainly via ajax callbacks for performing user actions.
- The front end must be entirely based on HTML/JS/CSS and jQuery / jQueryUI, employing AJAX callbacks


The following techniques / technologies shall not be employed:
- Any other client side framework such as AngularJS, BootStrap, KnokOut etc.
- Any persistence layer (such as hibernate or entity framework etc)
- Any control binding techniques / frameworks (e.g. as in MVVM etc)
- Any other RAD framework other than the bare tools offered by the IDE's in question.
- Any third party user interface controls (client or server side) apart from jQuery and jQueryUI ones.


Projects in Java shall be provided as Eclipse project
Projects in C# shall be provided as MS Visual Studio Express Solutions.

The desired functionality is about managing the employees and their attributes. Through the application one must be able to view,
create, modify, and delete employees. For each employee, one must be able to view, add, modify and delete employee attributes.

Both these views must be visible from the same “page”. At the top one would see the list of all employees. Selecting one employee,
under the employee listing view one would be able to edit the selected employee details (name, date of hire, supervisor). 
For the selected employee, one would be also able to see a listing of all employee’s attributes and from there to add, view, 
and modify those, under the same approach as for the employee. Selecting one attribute, a view with the attribute details is
presented and from there the name and value can be edited. It is also acceptable that editing would be done in popup div “frames”.

All data input must be properly validated both in the client as well as in the server side. All styling should be through CSS
files in the browser.


The functionality of the application shall be also available via JSON REST web services.

Note: It is not strictly required to follow the proposed page layout as long as all the needed functionality 
(implicit and explicit) is available in a user friendly manner, however enough acquaintance with AJAX / JS shall be demonstrated.
