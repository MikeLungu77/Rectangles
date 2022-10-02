using Moq;
using RectanglesBusiness.Controllers;
using RectanglesBusiness.Interfaces;
using RectanglesDataAccess.Models;

namespace RectanglesUnitTests
{
    [TestClass]
    public class RectanglesControllerTests
    {
        private RectanglesController _controller;

        private readonly Mock<IRectanglesService> _serviceMock = new Mock<IRectanglesService>();

        public RectanglesControllerTests()
        {
            _serviceMock.Setup(x => x.CreateGrid(It.IsAny<Size>()));
            _serviceMock.Setup(x => x.CreateRectangle(It.IsAny<Size>(), It.IsAny<Position>()));
            _serviceMock.Setup(x => x.FindRectangle(It.IsAny<Position>())).Returns(GetRectangle());
            _serviceMock.Setup(x => x.RemoveRectangle(It.IsAny<Position>()));
            _controller = new RectanglesController(_serviceMock.Object);
        }

        [TestMethod]
        public void CreateGrid_CallsService_Successfuly_When_Size_IsValid()
        {
            var size = new Size(10, 10);
            var result = _controller.CreateGrid(size);
            Assert.IsNull(result);
            _serviceMock.Verify(_ => _.CreateGrid(size), Times.Once);
            _serviceMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void CreateGrid_Does_Not_CallsService_When_Size_Has_Negative_Width()
        {
            var result = _controller.CreateGrid(new Size(-5, 10));
            Assert.IsNotNull(result);
            _serviceMock.Verify(_ => _.CreateGrid(It.IsAny<Size>()), Times.Never);
            _serviceMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void CreateGrid_Does_Not_CallsService_When_Size_Has_Negative_Height()
        {
            var result = _controller.CreateGrid(new Size(10, -10));
            Assert.IsNotNull(result);
            _serviceMock.Verify(_ => _.CreateGrid(It.IsAny<Size>()), Times.Never);
            _serviceMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void CreateREctangle_CallsService_Successfuly_When_Size_IsValid()
        {
            var size = new Size(5, 5);
            var position = new Position(1,1);
            var result = _controller.CreateRectangle(size, position);
            Assert.IsNull(result);
            _serviceMock.Verify(_ => _.CreateRectangle(size, position), Times.Once);
            _serviceMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void CreateGrid_Does_Not_CallsService_When_Size_IsInvalid()
        {
            var result = _controller.CreateRectangle(new Size(-5, 10), new Position(1,1));
            Assert.IsNotNull(result);
            _serviceMock.Verify(_ => _.CreateRectangle(It.IsAny<Size>(), It.IsAny<Position>()), Times.Never);
            _serviceMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void CreateGrid_Does_Not_CallsService_When_Position_IsInvalid()
        {
            var result = _controller.CreateRectangle(new Size(5, 5), new Position(-1, 1));
            Assert.IsNotNull(result);
            _serviceMock.Verify(_ => _.CreateRectangle(It.IsAny<Size>(), It.IsAny<Position>()), Times.Never);
            _serviceMock.VerifyNoOtherCalls();
        }

        private Rectangle GetRectangle()
        {
            return new Rectangle(new Size(5, 5), new Position(1, 1));
        }
    }
}