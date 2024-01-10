﻿namespace TodoListBlazorWasm.Features
{
	public class PagingLink
	{
        public string Text { get; set; }
        public int Page { get; set; }
        public bool Enabled { get; set; }
        public bool Active { get; set; }

		public PagingLink(int page, bool enabled, string text)
		{
			Text = text;
			Page = page;
			Enabled = enabled;
		}
	}
}
