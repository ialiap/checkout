using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Challenge.Services.Payment.Common.Infrastructure;
using Challenge.Services.Payment.Test.Fixture;
using Challenge.Services.Payment.Test.Helper;
using LUM.Services.Material.Common.Infrastructure;
using LUM.Services.Material.Model.Request;
using LUM.Services.Material.Model.Response;
using LUM.Services.Material.Service;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LUM.Services.Material.Test.IntegrationTest
{

    public class MaterialIntegrationTest : IntegrationTestBase, IClassFixture<DependencyResolverFixture>
    {
        private readonly IMaterialService _materialService;
        private readonly IMapper _mapper;
        private readonly Setting _setting;


        public MaterialIntegrationTest(DependencyResolverFixture fixture)
        {
            var serviceProvide = fixture.ServiceProvider;
            _materialService = serviceProvide.GetService<IMaterialService>();
            _mapper = serviceProvide.GetService<IMapper>();
            _setting = fixture.Setting;

        }
        [Fact]
        public async Task Crud_A_Material_Object_Must_Be_Successful()
        {
            var moq = MockHelper.MoqCreateMaterialBindingModel();
            var createId = await PostAsync(moq, $"{_setting.BaseUrl}/Material");
            Assert.True(Guid.TryParse(createId, out _));
            var getById = await GetAsync<GetMaterialResponseModel>($"{_setting.BaseUrl}/Material/{createId}");
            Assert.Equal(getById.Id, createId);
            getById.Name = "new name";
            var updateResult =
                await UpdateAsync(_mapper.Map<GetMaterialResponseModel, UpdateMaterialBindingModel>(getById), $"{_setting.BaseUrl}/Material");
            Assert.Equal(HttpStatusCode.OK, updateResult);

            var deleteResult = await DeleteAsync($"{_setting.BaseUrl}/Material/{getById.Id}");
            Assert.Equal(HttpStatusCode.NoContent, deleteResult);


        }

    }
}
