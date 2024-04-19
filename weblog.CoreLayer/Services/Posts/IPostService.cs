﻿using weblog.CoreLayer.DTOs.Posts;using weblog.CoreLayer.Utilities;namespace weblog.CoreLayer.Services.Posts;public interface IPostService{    OperationResult CreatePost(CreatePostDto command);    OperationResult EditPost(EditPostDto command);    PostDto GetPostById(int postId);    PostDto GetPostBySlug(string slug);    PostFilterDto GetPostsByFilter(PostFilterParams filterParams);    bool IsSlugExist(string slug);    List<PostDto> GetRelatedPosts(int groupId);    List<PostDto> GetPopularPosts();    void IncreaseVisit(int postId);}