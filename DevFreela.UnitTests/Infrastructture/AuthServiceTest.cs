using System.Collections;
using Bogus;
using DevFreela.Core.Enums;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Auth;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace DevFreela.UnitTests.Infrastructure
{

    public class GenerateJwtTokenDataError : IEnumerable<object[]>
    {
        private static readonly Faker faker = new("pt_BR");

        private readonly ICollection<object[]> _data = new List<object[]>
        {
            new object[] 
            {
                new Dictionary<string, string>(){},
                "A propriedade 'Jwt:Issuer' não foi devidamente configurada"
            },
            new object[] 
            {
                new Dictionary<string, string>
                {
                    {"Jwt:Issuer", faker.Lorem.Word()},
                },
                "A propriedade 'Jwt:Audience' não foi devidamente configurada"
            },
            new object[] 
            {
                new Dictionary<string, string>
                {
                    {"Jwt:Issuer", faker.Lorem.Word()},
                    {"Jwt:Audience", faker.Lorem.Word()}
                },
                "A propriedade 'Jwt:Key' não foi devidamente configurada"
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class AuthServiceTest
    {
        
        private IConfiguration configurationMock;
        private IAuthService authService;

        private static readonly Faker faker = new("pt_BR");

        public AuthServiceTest()
        {
            configurationMock = new ConfigurationBuilder().AddInMemoryCollection().Build();
            authService = new AuthService(configurationMock);
        }

        [Fact]
        public void ConfigureAllEnvs_Executed_ReturnTokenDTO()
        {
            // Arrange
            var env = new Dictionary<string, string>{
                {"Jwt:Issuer", faker.Lorem.Word()},
                {"Jwt:Audience", faker.Lorem.Word()},
                {"Jwt:Key", faker.Lorem.Letter(200)}
            };
            configurationMock = new ConfigurationBuilder()
                .AddInMemoryCollection(env)
                .Build();
            authService = new AuthService(configurationMock);
            var roles = new List<RoleNameEnum> {faker.PickRandom<RoleNameEnum>() };
            var email = faker.Internet.Email();
            // Act
            var actual = authService.GenerateJwtToken(email, roles);
            //Assert
            Assert.NotNull(actual);
            Assert.NotNull(actual.Token);
            Assert.True(actual.ExpiresIn > 0);
        }

        [Theory]
        [ClassData(typeof(GenerateJwtTokenDataError))]
        public void ConfigurationWithouKeys_Executed_ThrowError(Dictionary<string, string> configProps, string exceptionMessage)
        {
            // Arrange
            configurationMock = new ConfigurationBuilder().AddInMemoryCollection(configProps).Build();
            authService = new AuthService(configurationMock);
            var roles = new List<RoleNameEnum> {faker.PickRandom<RoleNameEnum>() };
            var email = faker.Internet.Email();
            // Act
            var actual = Assert.Throws<ArgumentException>(() => authService.GenerateJwtToken(email, roles));
            //Assert
            Assert.NotNull(actual);
            Assert.Equal(actual.Message, exceptionMessage);
        }

        [Fact]
        public void SendPasswordToEncrytp_Executed_ReturnHash(){
            // Arrange
            var password = faker.Lorem.Word();
            // Act
            var actual = authService.ComputeSha256Hash(password);
            //Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void SendEmptyPasswordToEncrytp_Executed_ThrowError(){
            // Arrange
            String password = null;
            // Act
            var actual = Assert.Throws<ArgumentNullException>(() => authService.ComputeSha256Hash(password));
            //Assert
            Assert.NotNull(actual);
        }

    }

}