using Discord;
using Discord.Commands;
using Vostok.Logging.Abstractions;

namespace OopRulerBot.DisscordControllers;

public class InfoController : ModuleBase<SocketCommandContext>
{
    private readonly ILog log;

    public InfoController(ILog log)
    {
        this.log = log;
    }

    [Command("say")]
    [Summary("Echoes a message.")]
    public async Task SayAsync([Remainder] [Summary("The text to echo")] string echo)
    {
        await ReplyAsync(echo);
    }

    [Command("role")]
    [Summary("Gives a role. Creates one if it doesn't exist now")]
    public async Task GiveRole([Remainder] [Summary("Role name")] string roleName)
    {
        if (Context.User is not IGuildUser guildUser)
            throw new InvalidOperationException("User is not a guild user");
        
        IRole? role = Context.Guild.Roles.SingleOrDefault(x => x.Name == roleName);
        if (role is null)
        {
            log.Info("Role {0} doesn't exist. Creating one", roleName);
            role = await Context.Guild.CreateRoleAsync(roleName);
            log.Info("Role {0} created", role.Mention);
        }

        await guildUser.AddRoleAsync(role);
        await ReplyAsync($"Gave role {role.Mention} to {guildUser.Mention}");
    }
}