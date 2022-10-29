//namespace DotNet_JWT
//{
//    public class APIBuilder : IAPIBuilder
//    {
//        private readonly WebApplicationBuilder _builder;

//        public APIBuilder(WebApplicationBuilder builder)
//        {
//            _builder = builder;
//        }

//        public void Configure(IServiceCollection services)
//        {
//            services.DbContextConfigure(_builder);
//            services.JWTConfigure(_builder);

//            services.AddTransient<IAuthService, AuthService>();

//            services.AddControllers();
//            services.AddEndpointsApiExplorer();
//            services.AddSwaggerGen();
//        }
//    }
//}
