using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace WpaGhApp.Services
{
    public interface IGitHubService
    {
        string LastErrorMessage { get; }
        string LastExceptionDetails { get; }

        void StartOAuthFlow();
        Task<bool> CompleteOAuthFlowAsync(string responseData);


        Task GetNewsAsync();
        Task<IReadOnlyList<Repository>> GetRepositoriesAsync();
        Task<IReadOnlyList<User>> GetFollowersAsync();
        Task<IReadOnlyList<User>> GetFollowingAsync();
    }
}
