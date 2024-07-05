namespace HanderBook
{
    public interface IHandlerService
    {
        Task<string> TransferSummarizeContentToShortStory(IFormFile file);
        Task<string> SummarizeBook(IFormFile file);
        Task<string> TransferTextToVideo(IFormFile file);
    }
}
