//create a new project
//use MVC

//Models: use for showing dataformat for database or input from user etc 
//view: the html code
//controller: add the routes here. needed manupulation done before passing it to the view

//adding mvc to a project
    //install microsoft mvc nuget package

//convention for making a the folders wrt mvc 
    //create a new folder for the main routes in view and create a new file for the model and controller
        //eg /product/laptops
            //create a folder called products in the views folder create index and laptop cshtml files
            //create a new file ProductController.cs in the controller folder
            //create a new Products.cs in models --> this should contain the format of the data be presented


<-------ViewModel------>
//if multiple objects have to be returned to the view
    //create a new ViewModels folder in the same dir
        //create a new class in this folder with the naming convention <ControllerName>ViewModel
        //in the file
            using <project name>.Models

            //in the class
                public <class name in model> <object name> {get;set;}
        
    //in the main controller //optional if using preset values
        //after assigning values to the objects to the models object
            <Model class name> <obj name>=new <Model class name>{
                para1=value1
            }

        //to set values to the view models
        //create a new obj for ViewModels
        <ViewModel class name> <obj>=<ViewModel class name>{
            <Model's class name>=<model's obj name>
        }
    //to return view
        return(<ViewModel obj name>)    


//creating the view(cshtml)
    @model <project name>.<folder>.<Classname>

    ...html code...
    //to use the values from backend(interpolation)
        <html tag> @Model.<property needed> </html tag>



//returning views other than index
    return View("<htmlfile name>")
//controller: the main class with ":controller" atached with it
//action: the functions with IActionResult return type with in the controller class


//<------------routing----------->
//in the controller file
//before every class which needs to have a seperate route add the route attribute
    [Route("<route url>")] //single word not the whole url
    [Route("")] //for default route
    [Route("<route url>/{<var passing>}")] //if value to be passed is optional create a seperate route without it 
    [Route("[controller]")] 
        //in the actual controller 
            Route["action"]//uses the class name of the controller before the Controller <URL used>Controller
    //showing contents in a file directly on webpage
        return PhysicalFile('<file path>',<type of file>)  //text/plain
    //<----redirecting------>
         //in the class
    	    return LocalRedirect(<url>) //redirecting within the site
            return Redirect(<url>) //redirecting to any site
            return RedirectToAction(<action name>) //redirecting to an action in that specific controller
            return RedirectToAction(<Controller name>,<action name>)

<----------------------------------HTTP Operations------------------------------------------>
//in the controller file
//above the IActionResult function under the main controller class add the Http<type of crud operation> attribute
//as the html form has the name property same as that of class data members

    [HttpGet] (or) [HttpPost]
    public IActionResult Index(<class name><obj name para>)
    {
        //create an obj 
        //if view
        <model class> <obj name>=new <model class>
        {
            <para1>=<obj name para>.<para1>
        };

        //if using viewmodel
        <viewmodel class> <obj name>=new <view model name>
        {
            <class name>=<obj name para>
        };
    }

    //html form's input name property has to match the prop names given in the model
    //in model file    
        public <Model name>
        {
            <data type> <prop1>;
            <data type> <prop2>
        }
    //in html file
    <form action="<redirect url>" method="GET/POST ">
        <input name="<prop1>" id="<id name>">
    </form>


    <-------------query string---------->
    //passing values using URL 
    //form method must be get request 
        //eg= localhost/Home/Index?id=3&FirstName=Mukundh
        //the args used in the query string must be the same as the model prop name
        //& to seperate diffrent args + acts as space
    //in the controller file
        use FromQuery attribute in the function
        publilc IActionResult <name>([FromQuery] <datatype> <arg>)
        {

        }
		
		

<----html form manupulation ie DataAnnotaion----->
//in the models file for which a html form is being created
	using System.ComponentModel.DataAnnotaion;
	
	//in the class
	public class <class name>
	{
		[<DataAnnotaion>]
		<data member>
	}


//<--------Entity framework----------->
//follow 
    //packages
        Install-Package Microsoft.EntityFrameworkCore.SqlServer
        Install-Package EntityFramework
        Install-Package Microsoft.EntityFrameworkCore.Tools

<----code first-----> //first model then database https://docs.microsoft.com/en-us/ef/core/get-started/aspnetcore/new-db
    //in the (create)enity folder create a new class file    
        //naming convention: <name>Context.cs
    //in the file
        using Microsoft.EntityFrameworkCore;
        //using System.Data.Entity do not use error in startup.cs
    //the class must inherite DbContext
    public class <Classname> : DbContext
    {
        //create a constructor
        public <class name>(DbContextOptions<<class name>> options):base(options) //ie options are passed to the base class
        {}
        //outside constructor
        public DbSet<<class name of the defined models>> class name in plural form { get; set; }
        
    }
        
//in the start up file paste using statments
    using Microsoft.EntityFrameworkCore;
    using <project name>.Models

    //<---------connect to db------>
    //click on SQL server object explorer
        //right click on local db and click on properties
        //copy the connection string
    //in the ConfigureServices function in startup.cs
        //var connection = @"<connection string of db>" //remove data after timeout i needed change the initial catelog to project name
        //services.AddDbContext<BloggingContext>(options => options.UseSqlServer(connection));

    //adding migration: enitity framework looks in the context and find the number of tables required based up on the models in the context file
        //in the package manager console
            EntityFrameworkCore(or)EntityFramewor\Enable-Migrations
            EntityFrameworkCore(or)EntityFramewor\Add-Migration <name of migration> //if ran into error restart vs
            EntityFrameworkCore(or)EntityFramewor\update-database


<---database first------> //first database then code https://docs.microsoft.com/en-us/ef/core/get-started/aspnetcore/existing-db
//install enitity framework and core
//open sql server object explorer copy the connection string of database
    //install the entity based sql packages including designs
//in the package manager console window
//connecting db with vs
    //in vs go to sql server object explorer
        //choose db select aouth and connect
        //right click on db and properties copy connection string
    Scaffold-DbContext "<connection string>" Microsoft.EntityFrameworkCore.SqlServer -OutputDir <destination folder>
    //remove this after timeout from connection string if needed
    //destination folder name: entities


<--------------------------CRUD operation using LINQ-------------------------->
//linq uses method and query syntax use method syntax more
//create a folder <project name>Repository //this folder will contain all the db call functions
    //create a new class file <Model name>Repository
    //in the repo class file
        public <return type> <function name>
        {
            <context name> <obj name>=new <context name>;
            //to convert to list use <whatever>.ToList();
        }

<----reading---->
    //one record
        .FirstOrDefault()
    //one or more
        .Where()
    //converting filtered data into list
        .ToList()

    //Query syntax
        //sql like syntax

//once the db is migrated  
//in the controller create a private var to access the entites of the
        
