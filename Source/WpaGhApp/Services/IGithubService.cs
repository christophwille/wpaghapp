using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;
using WpaGhApp.Models;

namespace WpaGhApp.Services
{
    public interface IGitHubService
    {
        string LastErrorMessage { get; }
        string LastExceptionDetails { get; }

        void StartOAuthFlow();
        Task<bool> CompleteOAuthFlowAsync(string responseData);


        Task GetNewsAsync();
        Task<IReadOnlyList<Repository>> GetRepositoriesAsync(string login);
        Task<IReadOnlyList<User>> GetFollowersAsync(string login);
        Task<IReadOnlyList<User>> GetFollowingAsync(string login);

        Task<IReadOnlyList<GitHubCommit>> GetCommitsAsync(IGitHubRepositoryIdentifiers repositoryIdentifiers);
        Task<IReadOnlyList<Issue>> GetIssuesAsync(IGitHubRepositoryIdentifiers repositoryIdentifiers);
    }
}
