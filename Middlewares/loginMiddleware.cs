// // // namespace OurMiddleware.Middlewares;

// // // using System.Diagnostics;


// namespace MyTask.Middlewares;

// public class loginMiddleware
// {
//     private RequestDelegate next;

//     public loginMiddleware(RequestDelegate next)
//     {
//         this.next = next;
//     }

//     public async Task Invoke(HttpContext c)
//     {
//         await c.Response.WriteAsync("Hello from our 1st nice middleware start\n");
//         await Task.Delay(1000);
//         await next(c);
//         await c.Response.WriteAsync("Hello from our 1st nice middleware end\n");      
//     }    
// }

// public static partial class MiddleExtensions
// {
//     public static IApplicationBuilder UseloginMiddleware(this IApplicationBuilder builder)
//     {
//         return builder.UseMiddleware<loginMiddleware>();
//     }
// }