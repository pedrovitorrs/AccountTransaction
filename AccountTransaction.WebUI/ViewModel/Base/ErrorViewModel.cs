namespace AccountTransaction.WebUI.ViewModel.Base
{
    public class ErrorViewModel
    {
        public int ErroCode { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public string? RequestId { get; internal set; }
    }
}