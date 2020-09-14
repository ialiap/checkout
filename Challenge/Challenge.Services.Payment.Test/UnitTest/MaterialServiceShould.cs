using System;
using System.Threading.Tasks;
using AutoMapper;
using Challenge.Services.Payment.Test.Fixture;
using Challenge.Services.Payment.Test.Helper;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Challenge.Services.Payment.Test.UnitTest
{
  
    public class MaterialIntegrationTest : IClassFixture<DependencyResolverFixture>
    {
        private readonly IMaterialService _materialService;
        private readonly IMapper _mapper;


        public MaterialIntegrationTest(DependencyResolverFixture fixture)
        {
            var serviceProvide = fixture.ServiceProvider;
            _materialService = serviceProvide.GetService<IMaterialService>();
            _mapper = serviceProvide.GetService<IMapper>();
        }
        [Fact]
        public async Task Create_New_Material_With_Valid_Data_Must_Be_Success()
        {
            var moq = MockHelper.MoqCreateMaterialBindingModel();
            var result = await _materialService.AddAsync(moq);
            Assert.True(Guid.TryParse(result, out _));
        }

        [Fact]
        public async Task Create_New_Material_With_Invalid_Data_Must_Be_Throw_ArgumentException()
        {
            var moq = MockHelper.MoqCreateMaterialBindingModel();
            moq.FunctionMaxTemperature = 100;
            await Assert.ThrowsAsync<ArgumentException>(() => _materialService.AddAsync(moq));
        }
        [Fact]
        public async Task Create_New_Material_With_Invalid_Temperature_Data_Must_Be_Throw_ArgumentException()
        {
            var moq = MockHelper.MoqCreateMaterialBindingModel();
            moq.FunctionMinTemperature = 30;
            moq.FunctionMaxTemperature = 10;
            await Assert.ThrowsAsync<AutoMapperMappingException>(() => _materialService.AddAsync(moq));
        }
        [Fact]
        public async Task Read_Material_By_Id_Returns_Single_Result()
        {
            var moq = MockHelper.MoqCreateMaterialBindingModel();
            var newlyInsertedMaterialId = await _materialService.AddAsync(moq);
            var refetchedMaterial = await _materialService.GetById(newlyInsertedMaterialId);
            Assert.NotNull(refetchedMaterial);
            Assert.Equal(refetchedMaterial.Id, newlyInsertedMaterialId);
        }

        [Fact]
        public async Task Read_Materials_By_Name_Returns_List_Of_Results()
        {
            var moq = MockHelper.MoqCreateMaterialBindingModel();
            var firstNewlyInsertedMaterialId = await _materialService.AddAsync(moq);
            var secondNewlyInsertedMaterialId = await _materialService.AddAsync(moq);
            var result = await _materialService.SearchByName(new SearchMaterialByNameQueryModel()
            {
                Name = moq.Name,
                OrderBy = nameof(moq.Name)
            });
            Assert.True(result.Items.Count(x => x.Name.Trim() == moq.Name.Trim()) >=1);
        }
        [Fact]
        public async Task Delete_Material_By_Id_Is_Successful()
        {
            var moq = MockHelper.MoqCreateMaterialBindingModel();
            var insertedResultId = await _materialService.AddAsync(moq);
            await _materialService.DeleteAsync(insertedResultId);
            var deletedItemResult = await _materialService.GetById(insertedResultId);
            Assert.Null(deletedItemResult);
        }


        [Fact]
        public async Task Update_Material_Is_Successful()
        {
            var moq = MockHelper.MoqCreateMaterialBindingModel();
            var insertedResultId = await _materialService.AddAsync(moq);
            var updateMaterialBindingModel =
                _mapper.Map<CreateMaterialBindingModel, UpdateMaterialBindingModel>(moq);
            updateMaterialBindingModel.Id = insertedResultId;
            updateMaterialBindingModel.Name = "Updated Name";
            updateMaterialBindingModel.FunctionMaxTemperature = 33;
            await _materialService.UpdateAsync(updateMaterialBindingModel);
            var fetchedMaterial = await _materialService.GetById(insertedResultId);
            Assert.Equal("Updated Name", fetchedMaterial.Name);
        }

    }
}
