using Moq;
using RectanglesBusiness.Controllers;
using RectanglesBusiness.Interfaces;
using RectanglesBusiness.Services;
using RectanglesDataAccess.Models;
using RectanglesDataAccess.Repositories.Interfaces;

namespace RectanglesUnitTests
{
    [TestClass]
    public class RectanglesServiceTests
    {
        private RectanglesService _service;

        private readonly Mock<IRectanglesRepository> _repositoryMock = new Mock<IRectanglesRepository>();

        public RectanglesServiceTests()
        {
            _repositoryMock.Setup(x => x.CreateGrid(It.IsAny<Size>()));
            _repositoryMock.Setup(x => x.CreateRectangle(It.IsAny<Rectangle>()));
            _repositoryMock.Setup(x => x.SaveRectangles(It.IsAny<List<Rectangle>>()));
            _service = new RectanglesService(_repositoryMock.Object);
        }

        [TestMethod]
        public void CreateGrid_CallsRepository()
        {
            var size = new Size(10, 10);
            _service.CreateGrid(size);
            _repositoryMock.Verify(_ => _.CreateGrid(size), Times.Once);
            _repositoryMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void CreateRectangle_Returns_Error_When_Grid_Does_NotExist()
        {
            var size = new Size(5, 5);
            var position = new Position(1,1);
            var rectangle = new Rectangle(size, position);
            _repositoryMock.Setup(x => x.GetGrid()).Returns<Grid>(null);
            var result = _service.CreateRectangle(size, position);
            Assert.IsNotNull(result);
            _repositoryMock.Verify(_ => _.GetGrid(), Times.Once);
            _repositoryMock.Verify(_ => _.GetRectangles(), Times.Once);
            _repositoryMock.Verify(_ => _.CreateRectangle(rectangle), Times.Never);
            _repositoryMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void CreateRectangle_Returns_Successfully_When_Grid_Exists_And_No_Other_Rectangles()
        {
            var size = new Size(5, 5);
            var position = new Position(1, 1);
            var rectangle = new Rectangle(size, position);
            _repositoryMock.Setup(x => x.GetGrid()).Returns(new Grid(new Size(10,10)));
            _repositoryMock.Setup(x => x.GetRectangles()).Returns(new List<Rectangle>());
            var result = _service.CreateRectangle(size, position);
            Assert.IsNotNull(result);
            Assert.AreEqual(string.Empty, result);
            _repositoryMock.Verify(_ => _.GetGrid(), Times.Once);
            _repositoryMock.Verify(_ => _.GetRectangles(), Times.Once);
            _repositoryMock.Verify(_ => _.CreateRectangle(It.IsAny<Rectangle>()), Times.Once);
            _repositoryMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void CreateRectangle_Returns_Error_When_Grid_Exists_And_Rectangle_WouldExtend_Beyond()
        {
            var size = new Size(5, 5);
            var position = new Position(8, 8);
            var rectangle = new Rectangle(size, position);
            _repositoryMock.Setup(x => x.GetGrid()).Returns(new Grid(new Size(10, 10)));
            _repositoryMock.Setup(x => x.GetRectangles()).Returns(new List<Rectangle>());
            var result = _service.CreateRectangle(size, position);
            Assert.IsNotNull(result);
            Assert.AreEqual("Rectangle would extend beyond grid boundaries", result);
            _repositoryMock.Verify(_ => _.GetGrid(), Times.Once);
            _repositoryMock.Verify(_ => _.GetRectangles(), Times.Once);
            _repositoryMock.Verify(_ => _.CreateRectangle(It.IsAny<Rectangle>()), Times.Never);
            _repositoryMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void CreateRectangle_Returns_Error_When_Grid_Exists_And_Rectangle_Would_OverLap_Existing()
        {
            var size = new Size(5, 5);
            var position = new Position(1, 1);
            var rectangle = new Rectangle(size, position);
            var existingRectangles = new List<Rectangle> { new Rectangle(new Size(5,5), new Position(4,4)) };
            _repositoryMock.Setup(x => x.GetGrid()).Returns(new Grid(new Size(10, 10)));
            _repositoryMock.Setup(x => x.GetRectangles()).Returns(existingRectangles);
            var result = _service.CreateRectangle(size, position);
            Assert.IsNotNull(result);
            Assert.AreEqual("This rectangle would overlap an existing one", result);
            _repositoryMock.Verify(_ => _.GetGrid(), Times.Once);
            _repositoryMock.Verify(_ => _.GetRectangles(), Times.Once);
            _repositoryMock.Verify(_ => _.CreateRectangle(It.IsAny<Rectangle>()), Times.Never);
            _repositoryMock.VerifyNoOtherCalls();
        }
    }
}