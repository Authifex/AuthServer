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

            // ��� DbContext
            builder.Services.AddDbContext<AuthifexDbContext>(options =>
                options.UseMySql(builder.Configuration.GetConnectionString("Default"),
                    new MySqlServerVersion(new Version(8, 0, 32))),
                ServiceLifetime.Scoped);

            // Add services to the container.
            // ��ӿ������� Swagger
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // �Զ�Ǩ�ƣ����������ɿ����������ɹرգ�
            if (app.Environment.IsDevelopment())
            {
                using (var scope = app.Services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<AuthifexDbContext>();
                    db.Database.Migrate(); // �Զ�Ӧ��δִ�е�Ǩ��
                }
            }

            // ��̬�ļ��������Ҫǰ����Դ��
            app.UseDefaultFiles();
            //app.UseStaticFiles();

            // HTTP �ܵ�
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