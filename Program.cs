
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using COMMON_PROJECT_STRUCTURE_API.services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;

WebHost.CreateDefaultBuilder().
ConfigureServices(s =>
{
    IConfiguration appsettings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
    s.AddSingleton<login>();
    s.AddSingleton<skillup_UserSignUp>();
    s.AddSingleton<skillup_UserProfile>();
    s.AddSingleton<skillup_UserSignIn>();
    s.AddSingleton<skillup_Course>();
    s.AddSingleton<skillup_Lesson>();
    s.AddSingleton<skillup_Video>();
    s.AddSingleton<upload>();
    s.AddSingleton<contact>();

    s.AddAuthorization();
    s.AddControllers();
    s.AddCors();
    s.AddAuthentication("SourceJWT").AddScheme<SourceJwtAuthenticationSchemeOptions, SourceJwtAuthenticationHandler>("SourceJWT", options =>
        {
            options.SecretKey = appsettings["jwt_config:Key"].ToString();
            options.ValidIssuer = appsettings["jwt_config:Issuer"].ToString();
            options.ValidAudience = appsettings["jwt_config:Audience"].ToString();
            options.Subject = appsettings["jwt_config:Subject"].ToString();
        });
}).Configure(app =>
{
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseCors(options =>
            options.WithOrigins("https://localhost:5002", "http://localhost:5001")
            .AllowAnyHeader().AllowAnyMethod().AllowCredentials());
    app.UseRouting();
    app.UseStaticFiles();

    app.UseEndpoints(e =>
    {
        var login = e.ServiceProvider.GetRequiredService<login>();
        var skillup_UserSignUp = e.ServiceProvider.GetRequiredService<skillup_UserSignUp>();
        var skillup_UserProfile = e.ServiceProvider.GetRequiredService<skillup_UserProfile>();
        var skillup_UserSignIn = e.ServiceProvider.GetRequiredService<skillup_UserSignIn>();
        var skillup_Course = e.ServiceProvider.GetRequiredService<skillup_Course>();
        var skillup_Lesson = e.ServiceProvider.GetRequiredService<skillup_Lesson>();
        var skillup_Video = e.ServiceProvider.GetRequiredService<skillup_Video>();
        var upload = e.ServiceProvider.GetRequiredService<upload>();
        var contact = e.ServiceProvider.GetRequiredService<contact>();

        e.MapPost("login",
     [AllowAnonymous] async (HttpContext http) =>
     {
                 var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                 requestData rData = JsonSerializer.Deserialize<requestData>(body);
                 if (rData.eventID == "1001") // update
                     await http.Response.WriteAsJsonAsync(await login.Login(rData));

             });
               e.MapPost("skillup_UserSignUp",
     [AllowAnonymous] async (HttpContext http) =>
     {
                 var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                 requestData rData = JsonSerializer.Deserialize<requestData>(body);
                 if (rData.eventID == "1001") // update
                     await http.Response.WriteAsJsonAsync(await skillup_UserSignUp.UserSignUp(rData));

             });
    
    e.MapPost("skillup_UserProfile",
     [AllowAnonymous] async (HttpContext http) =>
     {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // Insert
                    await http.Response.WriteAsJsonAsync(await skillup_UserProfile.CreateProfile(rData));
                if (rData.eventID == "1002") // Read
                    await http.Response.WriteAsJsonAsync(await skillup_UserProfile.ReadProfile(rData));
                if (rData.eventID == "1003") // update
                    await http.Response.WriteAsJsonAsync(await skillup_UserProfile.UpdateProfile(rData));
                if (rData.eventID == "1004") // Delete
                    await http.Response.WriteAsJsonAsync(await skillup_UserProfile.DeleteProfile(rData));

             });
    e.MapPost("skillup_UserSignIn",
     [AllowAnonymous] async (HttpContext http) =>
     {
                 var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                 requestData rData = JsonSerializer.Deserialize<requestData>(body);
                 if (rData.eventID == "1001") // update
                     await http.Response.WriteAsJsonAsync(await skillup_UserSignIn.UserSignIn(rData));

             });
    e.MapPost("skillup_Video",
     [AllowAnonymous] async (HttpContext http) =>
     {
                 var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                 requestData rData = JsonSerializer.Deserialize<requestData>(body);
                 if (rData.eventID == "1001") // update
                     await http.Response.WriteAsJsonAsync(await skillup_Video.Video(rData));

             });

    e.MapPost("skillup_Lesson",
     [AllowAnonymous] async (HttpContext http) =>
     {
                 var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                 requestData rData = JsonSerializer.Deserialize<requestData>(body);
                 if (rData.eventID == "1001") // update
                     await http.Response.WriteAsJsonAsync(await skillup_Lesson.Lesson(rData));

             });

    e.MapPost("skillup_Course",
     [AllowAnonymous] async (HttpContext http) =>
     {
                 var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                 requestData rData = JsonSerializer.Deserialize<requestData>(body);
                 if (rData.eventID == "1001") // update
                     await http.Response.WriteAsJsonAsync(await skillup_Course.Course(rData));

             });

        e.MapPost("upload",
  [AllowAnonymous] async (HttpContext http) =>
  {
                 var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                 requestData rData = JsonSerializer.Deserialize<requestData>(body);
                 if (rData.eventID == "1001") // update
                     await http.Response.WriteAsJsonAsync(await upload.Upload(rData));

             });

        e.MapPost("contact",
   [AllowAnonymous] async (HttpContext http) =>
   {
                 var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                 requestData rData = JsonSerializer.Deserialize<requestData>(body);
                 if (rData.eventID == "1005") // update
                     await http.Response.WriteAsJsonAsync(await contact.Contact(rData));

             });
             
        IConfiguration appsettings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        e.MapGet("/dbstring",
                  async c =>
                  {
                       dbServices dspoly = new dbServices();
                       await c.Response.WriteAsJsonAsync("{'mongoDatabase':" + appsettings["mongodb:connStr"] + "," + " " + "MYSQLDatabase" + " =>" + appsettings["db:connStrPrimary"]);
                   });

        e.MapGet("/bing",
          async c => await c.Response.WriteAsJsonAsync("{'Name':'Anish','Age':'26','Project':'COMMON_PROJECT_STRUCTURE_API'}"));
    });
}).Build().Run();
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
public record requestData
{
    [Required]
    public string eventID { get; set; }
    [Required]
    public IDictionary<string, object> addInfo { get; set; }
}

public record responseData
{
    public responseData()
    {
        eventID = "";
        rStatus = 0;
        rData = new Dictionary<string, object>();
    }
    [Required]
    public int rStatus { get; set; } = 0;
    public string eventID { get; set; }
    public IDictionary<string, object> addInfo { get; set; }
    public IDictionary<string, object> rData { get; set; }
}
