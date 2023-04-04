using BrainbayConsoleApp.Interfaces.Characters.Executors;
using Moq;
using Xunit;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestsProject.Characters.CommandHandler
{
    public class CharactersCommandHandlerTests
    {
        private readonly Mock<ICharactersQueryExecutor> _charactersQueryExecutorMock;

        public CharactersCommandHandlerTests(Mock<ICharactersQueryExecutor> charactersQueryExecutorMock)
        {
            _charactersQueryExecutorMock = charactersQueryExecutorMock;
        }
        
        [Fact]
        public async Task GetMovements_InternalApiIsCalled_ThirdPartyApiIsCalled()
        {
            // Arrang
            var responseContent = new PagedMovements();

            _moq.Setup(responseContent);

            // Act
            await _client.GetAsync($"/v1.0/GetMovements");

            //Assert
            _moq.Verify("");
        }
    }
}
