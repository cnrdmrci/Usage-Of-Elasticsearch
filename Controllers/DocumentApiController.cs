using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;
using Usage_Of_Elasticsearch.DocumentApi;
using Usage_Of_Elasticsearch.DocumentApi.MultiDocumentApi;

namespace Usage_Of_Elasticsearch.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DocumentApiController : ControllerBase
    {
        #region Single Document Api

        [HttpPut("CreateIndex")]
        public async Task<IActionResult> CreateIndex()
        {
            IndexApi indexApi = new IndexApi();
            bool response = await indexApi.CreateIndexAsync<User>(2, 1, ConstantStrings.IndexName, ConstantStrings.IndexName + "_alias");
            if (response)
                return Ok(ConstantStrings.IndexName + " index has been created.");

            return BadRequest(ConstantStrings.IndexName + " could not create.");
        }

        [HttpPost("AddData")]
        public async Task<IActionResult> AddData()
        {
            User newUser = new User
            {
                Id = 1,
                UserName = "cnrdmrci",
                FirstName = "Caner",
                LastName = "LastName",
                Age = 20,
                Gender = "M"
            };

            IndexApi indexApi = new IndexApi();
            bool response = await indexApi.AddDataAsync(ConstantStrings.IndexName, newUser);
            if(response)
                return Ok(newUser.UserName + " username has been added.");

            return BadRequest(newUser.UserName + " could not added.");
        }

        [HttpPost("AddDataWithJson")]
        public async Task<IActionResult> AddDataWithJson([FromBody] User data)
        {
            IndexApi indexApi = new IndexApi();
            bool response = await indexApi.AddDataAsync(ConstantStrings.IndexName, data);
            if (response)
                return Ok(data.UserName + " username has been added.");

            return BadRequest(data.UserName + " could not added.");
        }

        [HttpGet("GetUserDataById/{id}")]
        public async Task<IActionResult> GetUserDataById(int id)
        {
            GetApi getApi = new GetApi();
            var response = await getApi.GetDataByIdAsync<User>(ConstantStrings.IndexName,id);
            if (response == null)
                return BadRequest(id + " id could not found.");

            return Ok(response);
        }

        [HttpGet("GetUserDataByUserName/{userName}")]
        public async Task<IActionResult> GetUserDataById(string userName)
        {
            GetApi getApi = new GetApi();
            var response = await getApi.GetUserDataByUserNameAsync(ConstantStrings.IndexName,userName);
            if (response == null)
                return BadRequest(userName + " username could not found.");

            return Ok(response);
        }

        [HttpPost("UpdateData")]
        public async Task<IActionResult> UpdateData()
        {
            User user = new User
            {
                Id = 1,
                UserName = "cnrdmrci",
                FirstName = "Caner",
                LastName = "LastName updated",
                Age = 20,
                Gender = "M"
            };

            UpdateApi updateApi = new UpdateApi();
            var response = await updateApi.UpdateDataByIdAsync(ConstantStrings.IndexName, user, user.Id);
            if (response)
                return Ok(user.UserName + " username has been updated.");

            return BadRequest(user.UserName + " could not updated.");
        }

        [HttpDelete("DeleteUserDataById/{id}")]
        public async Task<IActionResult> DeleteUserDataById(int id)
        {
            DeleteApi deleteApi = new DeleteApi();
            var response = await deleteApi.DeleteDataByIdAsync<User>(ConstantStrings.IndexName,id);
            if (response)
                return Ok(id + " id has been removed.");

            return BadRequest(id + " id could not removed.");
        }

        #endregion

        #region Multi Document Api

        [HttpGet("MultiGetDataByIds")]
        public async Task<IActionResult> MultiGetDataByIds()
        {
            List<string> ids = new List<string>{"1","5"};
            MultiGetApi multiGetApi = new MultiGetApi();
            List<User> response = await multiGetApi.MultiGetDataByIds<User>(ConstantStrings.IndexName, ids);

            return Ok(response);
        }

        [HttpPost("Reindex")]
        public async Task<IActionResult> Reindex()
        {
            ReindexApi reindexApi = new ReindexApi();
            var response = await reindexApi.ReindexSourceToDestionation(ConstantStrings.IndexName, "example_reindex");
            return Ok(response);
        }

        [HttpPost("AddBulkData")]
        public async Task<IActionResult> AddBulkData()
        {
            var users = new[]
            {
                new User
                {
                    Id = 3,
                    UserName = "user3",
                    FirstName = "First user 3",
                    LastName = "LastName3",
                    Age = 23,
                    Gender = "M"
                },
                new User
                {
                    Id = 4,
                    UserName = "user4",
                    FirstName = "First user 4",
                    LastName = "LastName4",
                    Age = 24,
                    Gender = "M"
                },
                new User
                {
                    Id = 5,
                    UserName = "user5",
                    FirstName = "First user 5",
                    LastName = "LastName5",
                    Age = 25,
                    Gender = "M"
                }
                // snip
            };

            BulkApi bulkApi = new BulkApi();
            var response = await bulkApi.AddBulkData(ConstantStrings.IndexName, users);

            return Ok(response);
        }

        [HttpPost("UpdateByQuery")]
        public async Task<IActionResult> UpdateByQuery()
        {
            UpdateByQueryApi updateByQueryApi = new UpdateByQueryApi();
            var response = await updateByQueryApi.UpdateByQueryAsync(ConstantStrings.IndexName);
            var response2 = await updateByQueryApi.UpdateByQueryWithUserNameAsync<User>(ConstantStrings.IndexName);
            return Ok(response);
        }

        [HttpDelete("DeleteByQuery/{userName}")]
        public async Task<IActionResult> DeleteByQuery(string userName)
        {
            DeleteByQueryApi deleteByQueryApi = new DeleteByQueryApi();
            var response = await deleteByQueryApi.DeleteByQueryAsync<User>(ConstantStrings.IndexName, userName);
            return Ok(response);
        }
        #endregion

    }
}