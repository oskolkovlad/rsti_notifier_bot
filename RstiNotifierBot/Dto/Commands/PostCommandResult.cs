namespace RstiNotifierBot.Dto.Commands
{
    internal class PostCommandResult : CommandResult
    {
        public PostCommandResult(PostDto post) : base(true)
        {
            Post = post;
        }

        public PostDto Post { get; private set; }
    }
}