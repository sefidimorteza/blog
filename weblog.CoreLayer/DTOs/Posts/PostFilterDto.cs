﻿namespace weblog.CoreLayer.DTOs.Posts;public class PostFilterDto{
    public int PageCount { get; set; }    public List<PostDto> Posts { get; set; }
    public PostFilterParams FilterParams { get; set; }
}public class PostFilterParams{    public string CategorySlug { get; set; }    public int PageId { get; set; }    public int Take { get; set; }    public string Title { get; set; }}