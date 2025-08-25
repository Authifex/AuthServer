using Authifex.DataAccess.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Authifex.Web.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 添加 DbContext
            builder.Services.AddDbContext<AuthifexDbContext>(options =>
                options.UseMySql(builder.Configuration.GetConnectionString("Default"),
                    new MySqlServerVersion(new Version(8, 0, 32))),
                ServiceLifetime.Scoped);

            // Add services to the container.
            // 添加控制器和 Swagger
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // 自动迁移（开发环境可开启，生产可关闭）
            if (app.Environment.IsDevelopment())
            {
                using (var scope = app.Services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<AuthifexDbContext>();
                    db.Database.Migrate(); // 自动应用未执行的迁移
                }
            }

            // 静态文件（如果需要前端资源）
            app.UseDefaultFiles();
            //app.UseStaticFiles();

            // HTTP 管道
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            //app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}