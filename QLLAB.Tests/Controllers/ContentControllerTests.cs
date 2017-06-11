using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using QLLAB.Controllers;
using QLLAB.Models;
using QLLAB.Repositories.Interfaces;
using QLLAB.Services.Interfaces;
using Xunit;

namespace QLLAB.Tests.Controllers
{
    public class ContentControllerTests
    {
        private ContentController _sut;
        private readonly Mock<IBlobStorageImageService> _blobStorageImageServiceMock;
        private readonly Mock<IImageRepository> _imageRepositoryMock;
        private readonly Mock<IOptions<AppSettings>> _optionsMock;
        private const string ImageData = "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";
        private const string FileName = "myimage.png";
        private const string Tags = "nyckelpiga";
        private const string ImageBaseUrl = "http://www.mycdn.com/{0}";
        private readonly Guid _blobId;

        public ContentControllerTests()
        {
            _blobStorageImageServiceMock = new Mock<IBlobStorageImageService>();
            _imageRepositoryMock = new Mock<IImageRepository>();
            _optionsMock = new Mock<IOptions<AppSettings>>();
            _blobId = Guid.NewGuid();
        }

        [Fact]
        public async Task Post_ContentWithDataAndPropertiesSet_ImageIsSaved()
        {
            //Arrange
            var content = new Content { Data = ImageData, Filename = FileName, Tags = Tags };
            var image = new Image();

            _sut = new ContentController(_blobStorageImageServiceMock.Object, _imageRepositoryMock.Object, _optionsMock.Object);

            _optionsMock.Setup(o => o.Value).Returns(new AppSettings { ImageBasePath = ImageBaseUrl });

            _imageRepositoryMock.Setup(i => i.SaveImageAsync(It.IsAny<Image>())).Returns(Task.FromResult(new Image())).
                Callback<Image>(obj => image = obj);

            _blobStorageImageServiceMock.Setup(b => b.SaveAsync(It.IsAny<Content>())).Returns(Task.FromResult(new Content
            {
                Tags = content.Tags,
                Filename = content.Filename,
                Data = content.Data,
                BlobId = _blobId
            }));

            //Act
            var result = await _sut.Post(content);

            //Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            
            var contentResult = (Content)okResult.Value;
            Assert.NotNull(contentResult?.Data);
            Assert.True(contentResult.Data.Equals(ImageData));
            Assert.True(contentResult.Filename.Equals(FileName));
            Assert.True(contentResult.Tags.Equals(Tags));
            Assert.True(image.Url.Equals(string.Format(ImageBaseUrl, _blobId)));

            _imageRepositoryMock.Verify(n => n.SaveImageAsync(It.IsAny<Image>()), Times.Once);
            _blobStorageImageServiceMock.Verify(n => n.SaveAsync(content), Times.Once);
        }
    }
}
