using Core.DTO.BusinessCardDTO;
using Core.Entity;
using Core.IServicesl;
using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Common;
using ProgressSoft.Controllers;

namespace UnitTset
{
    [TestClass]
    public class UnitTest1
    {
       
        private readonly IBusinessCardServices _services;

        
        public UnitTest1()
        {
            _services = A.Fake<IBusinessCardServices>();
        }
        [TestMethod]
        public async Task GetBusinessCardsServicesForList_ShouldReturnExpectedBusinessCards()
        {
            // Arrange
            var model = new BusinessCardForRequest
            {
                Name = "www",
            };
            var expectedBusinessCards = new List<BusinessCard>
            {
                new BusinessCard
                {
                    Name = "www",
                    Phone = "123456789",
                    Gender = "Male",
                    Email = "john@example.com",
                    Address = "Address 1",
                    DateOfBirth = DateTime.Now
                },
                new BusinessCard
                {
                    Name = "Jane Doe",
                    Phone = "987654321",
                    Gender = "Female",
                    Email = "jane@example.com",
                    Address = "Address 2",
                    DateOfBirth = DateTime.Now
                }
            };

            // Setup the mock service to return the expected list
            A.CallTo(() =>  _services.GetAllBusinessCard(model));

            // Create the controller with the mocked service
            var controller = new BusinessCardController(_services);

            // Act
            var finalResult = await controller.Get(model);

            // Assert
            finalResult.Should().NotBeNull();
            
        }

        [TestMethod]
        public async Task AddBusinessCardsServices_ShouldAddSuccessfully_WhenNoDuplicatesExist()
        {
            // Arrange: Prepare the input model for a new business card
            var newItem = new BusinessCardDetails
            {
                Name = "New User",
                Email = "newuser@example.com",
                Phone = "111222333",
                Gender = "Male",
                Address = "123 Main Street",
                DateOfBirth = System.DateTime.Now
            };

            // Mocking to ensure no duplicates exist in the database
            A.CallTo(() => _services.AddBusinessCard(newItem));

            // Act: Call the service method to add the business card
            var result = await _services.AddBusinessCard(newItem);

            // Assert: Verify the result
            result.IsSuccessful.Should().BeTrue(); // The operation should be successful
            result.ErrorCodes.Should().BeEmpty(); // There should be no error codes

        }

        [TestMethod]
        public async Task DeleteBusinessCardsServices_ShouldDeleteSuccessfully_WhenItemExists()
        {
            // Arrange
            int businessCardId = 1;
            var existingCard = new BusinessCard { Id = businessCardId, Name = "John Doe" };

            // Simulate that GetSingle returns an existing business card
            A.CallTo(() => _services.DeleteBusinessCard(businessCardId));

            // Act
            var result = await _services.DeleteBusinessCard(businessCardId);

            // Assert
            result.IsSuccessful.Should().BeTrue(); // Operation should be successful
            result.ErrorCodes.Should().BeEmpty(); // There should be no error codes

            
        }

    }
}