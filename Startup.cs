using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SchoolOfNet_API_Rest_com_ASPNET_Core_2.Data;
using Microsoft.EntityFrameworkCore;

namespace SchoolOfNet_api_rest_asp_net_core_hateoas
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")) );
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //SWAGGER
            //Mapear os controllers.
            services.AddSwaggerGen(config => {
                //ATENÇÃO, o primeiro parametro temq ue ser com v minúsculo, senão dar erro.
                //config.SwaggerDoc("V1", new Microsoft.OpenApi.Models.OpenApiInfo {Title= "API de Produtos . ", Version= "V1"});
                config.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {Title="API DE PRODUTOS",Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            //Informar ao AP.NEt que queremos usar o SWAGGER no projeto.
            app.UseSwagger(config =>{
                //Alterar o local onde o arquivo será gerado.
                //"documentName" representa a versão
                config.RouteTemplate = "documentacao/{documentName}/swagger.json";
            });/*Este metodo gera um arquivo JSON(swagger.json)*/
            //Cria a saida html
            app.UseSwaggerUI(config =>{
                //Vai gerar o HTML com base no json padrão do SWagger
                config.SwaggerEndpoint("/documentacao/v1/swagger.json", "My API V1");
                //Para deixar o swagger na rota padrão.
                config.RoutePrefix = string.Empty;
            });
        }
    }
}
