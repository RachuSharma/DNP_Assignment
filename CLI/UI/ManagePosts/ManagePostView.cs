﻿using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePostView
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;

    public ManagePostView(IPostRepository postRepository, IUserRepository userRepository)
    {
        this._postRepository = postRepository;
        this._userRepository = userRepository;
    }
    public async Task showAsync()
    {
        Console.WriteLine();
        await ManagePostAsync();
    }

    private async Task ManagePostAsync()
    {
        while (true)
        {
            Console.WriteLine("Plwase select option ");
            Console.WriteLine("Press 5 for Create Post :");
            Console.WriteLine("Press 6 for See list of Post :");
            Console.WriteLine("Press 7 for See Single Post :");

            string? inputFromUser = Console.ReadLine();

            switch (inputFromUser)
            {
                case "5":
                    CreatePostView createPostView = new CreatePostView(_postRepository,_userRepository);
                    await createPostView.ShowAsync();
                    return;
                case "6":
                    ListPostView listPostView = new ListPostView(_postRepository);
                    await listPostView.showAsync();
                    return;
                case "7":
                    SinglePostView singlePostView = new SinglePostView(_postRepository);
                    await singlePostView.showAsync();
                    return;
                default:
                    Console.WriteLine("Select valid option");
                    break;
            }

        }
    }
}