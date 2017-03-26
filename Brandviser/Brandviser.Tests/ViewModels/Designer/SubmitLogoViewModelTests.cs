using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Web.Areas.Designer.Models;
using NUnit.Framework;

namespace Brandviser.Tests.ViewModels.Designer
{
    [TestFixture]
    public class SubmitLogoViewModelTests
    {
        [Test]
        public void ConstructorShouldCreate_SubmitLogoViewModel_WithoutParams()
        {
            // Act & Assert
            var submitLogoViewModel = new SubmitLogoViewModel();

            Assert.IsInstanceOf<SubmitLogoViewModel>(submitLogoViewModel);
        }


        [Test]
        public void SubmitLogoViewModel_Should_Set_Properties_Correctly_With_CorrectData()
        {
            // Arrange
            var name = "name";
            var submitLogoViewModel = new SubmitLogoViewModel() { Name = name };

            // Act & Assert
            Assert.AreEqual(name, submitLogoViewModel.Name);
        }
    }
}
