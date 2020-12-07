using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using BookStore.WebApp.Controllers;
using BookStore.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BookStore.Tests
{
    public class CustomerControllerTests
    {
        [Fact]
        public void Index_WithCustomers_DisplaysCustomers()
        {
            // arrange
            var mockRepository = new Mock<IStoreRepository>();

            mockRepository.Setup(r => r.GetCustomers())
                .Returns(new[] {
                    new Customer {
                        ID=111,
                        FirstName = "TestFirstOne",
                        LastName = "TestLastOne",
                        MyStoreLocation = new Location{ LocationName = "Test Location"}
                    },
                    new Customer {
                        ID=555,
                        FirstName = "TestFirstFive",
                        LastName = "TestLastFive",
                        MyStoreLocation = new Location{ LocationName = "Test Location"}
                    }
            });

            var controller = new CustomerController(mockRepository.Object);

            // act
            IActionResult actionResult = controller.Index();

            //assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(actionResult);
            var customers = Assert.IsAssignableFrom<IEnumerable<CustomerViewModel>>(viewResult.Model);
            var customerList = customers.ToList();
            Assert.Equal(2, customerList.Count);
            Assert.Equal("TestFirstOne", customerList[0].FirstName);
            Assert.Equal(555, customerList[1].ID);
            Assert.Null(viewResult.ViewName);
        }
    }
}
