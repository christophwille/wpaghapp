using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;
using WpaGhApp.Models;
using WpaGhApp.Services;

namespace WpaGhApp.Github
{
    public partial class GhService : IGitHubService
    {
        private readonly IMessageService _messageService;
        private const string ProductHeaderName = "OctoCentral";
        private const string ProductHeaderValue = "1.0.0.0";

        private readonly GitHubClient _gitHubClient;

        public GhService(IMessageService messageService)
        {
            _messageService = messageService;
            _gitHubClient = new GitHubClient(new ProductHeaderValue(ProductHeaderName, ProductHeaderValue))
            {
                Credentials = Credentials.Anonymous
            };
        }

        private string _lastErrorMessage;
        private string _lastErrorExceptionDetails;
        void SetLastError(Exception ex)
        {
            if (null == ex)
            {
                _lastErrorMessage = String.Empty;
                _lastErrorExceptionDetails = String.Empty;
                return;
            }

            _lastErrorMessage = ex.Message;
            _lastErrorExceptionDetails = ex.ToString();
            Debug.WriteLine(_lastErrorExceptionDetails);
        }

        public string LastErrorMessage { get { return _lastErrorMessage; } }
        public string LastExceptionDetails { get { return _lastErrorExceptionDetails; } }

        async Task<T> ExecuteWithErrorTrappingAsync<T>(Func<Task<T>> func) where T : class
        {
            try
            {
                SetLastError(null);
                T computed = await func().ConfigureAwait(false);
                return computed;
            }
            catch (AuthorizationException ex)
            {
                // on eg invalid bearer token
                SetLastError(ex);
            }
            catch (Exception ex)
            {
                SetLastError(ex);
            }

            return null;
        }

        async Task EnsureCredentialsAsync()
        {
            // Not set yet or re-set because of expiration / need of retry
            if (_gitHubClient.Credentials == Credentials.Anonymous)
            {
                var credentials = await GetCredentialsAsync().ConfigureAwait(false);
                _gitHubClient.Credentials = credentials;
            }
        }

        public async Task GetNewsAsync()
        {
            await EnsureCredentialsAsync().ConfigureAwait(false);

            Feed f = await _gitHubClient.Activity.Feeds.GetFeeds().ConfigureAwait(false);

            string rss = f.CurrentUserUrl;

            // TODO: Research loading news (private timeline) for user

            // Double-drat (not working with OAuth it seems): https://developer.github.com/v3/activity/feeds/

            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Repository>> GetRepositoriesAsync(string login)
        {
            await EnsureCredentialsAsync().ConfigureAwait(false);

            var repositories = await ExecuteWithErrorTrappingAsync(async () =>
            {
                if (String.IsNullOrWhiteSpace(login))
                {
                    return await _gitHubClient.Repository.GetAllForCurrent().ConfigureAwait(false);
                }
                return await _gitHubClient.Repository.GetAllForUser(login).ConfigureAwait(false);
            })
            .ConfigureAwait(false);

            return repositories;
        }

        public async Task<IReadOnlyList<User>> GetFollowersAsync(string login)
        {
            await EnsureCredentialsAsync().ConfigureAwait(false);

            var followers = await ExecuteWithErrorTrappingAsync(async () =>
            {
                if (String.IsNullOrWhiteSpace(login))
                {
                    return await _gitHubClient.User.Followers.GetAllForCurrent().ConfigureAwait(false);
                }
                return await _gitHubClient.User.Followers.GetAll(login).ConfigureAwait(false);
            })
            .ConfigureAwait(false);

            return followers;
        }

        public async Task<IReadOnlyList<User>> GetFollowingAsync(string login)
        {
            await EnsureCredentialsAsync().ConfigureAwait(false);

            var following = await ExecuteWithErrorTrappingAsync(async () =>
            {
                if (String.IsNullOrWhiteSpace(login))
                {
                    return await _gitHubClient.User.Followers.GetFollowingForCurrent().ConfigureAwait(false);
                }
                return await _gitHubClient.User.Followers.GetFollowing(login).ConfigureAwait(false);
            })
            .ConfigureAwait(false);

            return following;
        }

        public async Task<IReadOnlyList<Organization>> GetOrganizationsAsync(string login)
        {
            await EnsureCredentialsAsync().ConfigureAwait(false);

            var orgs = await ExecuteWithErrorTrappingAsync(async () =>
            {
                if (String.IsNullOrWhiteSpace(login))
                {
                    return await _gitHubClient.Organization.GetAllForCurrent().ConfigureAwait(false);
                }
                return await _gitHubClient.Organization.GetAll(login).ConfigureAwait(false);
            })
            .ConfigureAwait(false);

            return orgs;
        }

        public async Task<IReadOnlyList<User>> GetOrganizationMembersAsync(string org)
        {
            await EnsureCredentialsAsync().ConfigureAwait(false);

            var members = await ExecuteWithErrorTrappingAsync(async () => 
                await _gitHubClient.Organization.Member.GetAll(org).ConfigureAwait(false))
                .ConfigureAwait(false);

            return members;
        }

        public async Task<IReadOnlyList<GitHubCommit>> GetCommitsAsync(IGitHubRepositoryIdentifiers repositoryIdentifiers)
        {
            await EnsureCredentialsAsync().ConfigureAwait(false);

            var commits = await ExecuteWithErrorTrappingAsync(() => _gitHubClient.Repository.Commits.GetAll(repositoryIdentifiers.RepositoryOwner, repositoryIdentifiers.RepositoryName))
                .ConfigureAwait(false);

            return commits;
        }

        public async Task<IReadOnlyList<Issue>> GetIssuesAsync(IGitHubRepositoryIdentifiers repositoryIdentifiers)
        {
            await EnsureCredentialsAsync().ConfigureAwait(false);

            var issues = await ExecuteWithErrorTrappingAsync(() => _gitHubClient.Issue.GetForRepository(repositoryIdentifiers.RepositoryOwner, repositoryIdentifiers.RepositoryName))
                .ConfigureAwait(false);

            return issues;
        }

        public async Task<IReadOnlyList<RepositoryContent>> GetContentsAsync(IGitHubRepositoryIdentifiers repositoryIdentifiers, string contentPath)
        {
            await EnsureCredentialsAsync().ConfigureAwait(false);

            var contents = await ExecuteWithErrorTrappingAsync(
                () => _gitHubClient.Repository.Content.GetContents(repositoryIdentifiers.RepositoryOwner, repositoryIdentifiers.RepositoryName, contentPath))
                .ConfigureAwait(false);

            return contents;
        }

        public async Task<TreeResponse> GetTreeRecursiveAsync(IGitHubRepositoryIdentifiers repositoryIdentifiers, string sha)
        {
            await EnsureCredentialsAsync().ConfigureAwait(false);

            var response = await ExecuteWithErrorTrappingAsync(
                () => _gitHubClient.GitDatabase.Tree.GetRecursive(repositoryIdentifiers.RepositoryOwner, repositoryIdentifiers.RepositoryName, sha))
                .ConfigureAwait(false);

            return response;
        }

        public async Task<IReadOnlyList<Branch>> GetBranchesAsync(IGitHubRepositoryIdentifiers repositoryIdentifiers)
        {
            await EnsureCredentialsAsync().ConfigureAwait(false);

            var branches = await ExecuteWithErrorTrappingAsync(() => _gitHubClient.Repository.GetAllBranches(repositoryIdentifiers.RepositoryOwner, repositoryIdentifiers.RepositoryName))
                .ConfigureAwait(false);

            return branches;
        }
    }
}
