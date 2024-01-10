using BlazorBootstrap;

namespace TodoListBlazorWasm.Utilities
{
    public class ViewUtils
    {
        public static ToastMessage CreateToastMessage(ToastType toastType, string title, string message)
            => new ToastMessage
                {
                    Type = toastType,
                    Title = title,
                    HelpText = $"{DateTime.Now}",
                    Message = message,
                };

		public static ToastMessage CreateToastMessage(ToastType toastType, string message)
	        => new ToastMessage
	            {
		            Type = toastType,
				    HelpText = $"{DateTime.Now}",
				    Message = message,
	            };
	}
}
