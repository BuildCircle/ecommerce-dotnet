using BuildCircleKata.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xunit;

namespace BuildCircleKata.test
{
    public class GivenAListOfItems
    {
        [Fact]
        public void ThenCanGetAllItemsWithOk()
        {
            var controller = new ProductController();

            var response = controller.getAllItems();

            response.Should().BeOfType(typeof(List<Product>));
        }

        [Fact]
        public void ThenCanGetAnSpecificItemById()
        {
            var controller = new ProductController();

            var responseId = controller.GetId(1);

            var okResult = Assert.IsType<OkObjectResult>(responseId);
            var body = (Product)okResult.Value;

            body.name.Should().Be(controller.getAllItems()[0].name);
        }

        [Fact]
        public void ThenCanGetAnSpecificItemByName()
        {
            var controller = new ProductController();

            var responseId = controller.GetByName("Apple");

            var okResult = Assert.IsType<OkObjectResult>(responseId);

            var body = (Product)okResult.Value;

            body.name.Should().Be(controller.getAllItems()[0].name);
        }

        [Fact]
        public void ThenCanFilterItemsByTag()
        {
            var controller = new ProductController();

            var response = controller.GetByTags("fruit");

            var okResult = Assert.IsType<OkObjectResult>(response);

            var returnValue = Assert.IsType<List<Product>>(okResult.Value);

            returnValue.Count.Should().Be(2);

        }

    }
}
