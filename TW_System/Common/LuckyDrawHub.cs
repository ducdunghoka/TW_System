using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Threading;

namespace TW_System.Common
{
    public class LuckyDrawHub : Hub
    {
        //private readonly MiniSoftContext db;
        //public static int _page = 1;
        //public LuckyDrawHub(MiniSoftContext context)
        //{
        //    db = context;
        //}
        //public async Task ControlSetPage(string id, int pageId, string connectionId)
        //{
        //    var program = db.LKD_Programs.FirstOrDefault(x => x.ControlId.ToString() == id);
        //    if (program == null)
        //    {
        //        await Clients.Client(connectionId).SendAsync("Exception", "Không tìm thấy chương trình");
        //        return;
        //    }
        //    var main = db.LKD_Mains.FirstOrDefault(x => x.ProgramId == program.Id && x.Id == pageId);
        //    if (main == null)
        //    {
        //        await Clients.Client(connectionId).SendAsync("Exception", "Không tìm thấy trang");
        //        return;
        //    }
        //    program.Page = main.No;
        //    await db.SaveChangesAsync();
        //    await Clients.Group(program.Id.ToString()).SendAsync("DisplayLoadPage", main.No);
        //}

        //public async Task DisplayPage(string id)
        //{
        //    var program = db.LKD_Programs.FirstOrDefault(x => x.DisplayId.ToString() == id);
        //    await Clients.Group(program.Id.ToString()).SendAsync("DisplayPage", program.Page ?? 1);
        //}

        //public async Task ControlChooseTurn(string id, int pageId, int turnNo, string connectionId)
        //{
        //    var program = db.LKD_Programs.FirstOrDefault(x => x.ControlId.ToString() == id);
        //    if (program == null)
        //    {
        //        await Clients.Client(connectionId).SendAsync("Exception", "Không tìm thấy chương trình");
        //        return;
        //    }
        //    await Clients.Group(program.Id.ToString()).SendAsync("DisplayClearReward", turnNo);
        //    if (turnNo == 0)
        //    {
        //        await Clients.Group(program.Id.ToString()).SendAsync("TabletShowDraw", null, null, null, null);
        //    }

        //    var main = db.LKD_Mains.FirstOrDefault(x => x.ProgramId == program.Id && x.Id == pageId);
        //    if (main == null)
        //    {
        //        await Clients.Client(connectionId).SendAsync("Exception", "Không tìm thấy trang");
        //        await Clients.Group(program.Id.ToString()).SendAsync("TabletShowDraw", null, null, null, null);
        //        return;
        //    }
        //    _page = main.No;
        //    var reward = db.LKD_Rewards.FirstOrDefault(x => x.Id == main.RewardId);
        //    if (reward == null)
        //    {
        //        await Clients.Group(program.Id.ToString()).SendAsync("TabletShowDraw", null, null, null, null);
        //        return;
        //    }

        //    if (reward.Turn < turnNo)
        //    {
        //        await Clients.Client(connectionId).SendAsync("Exception", "Lượt quay được chọn lớn hơn số lượng lượt tối đa");
        //        await Clients.Group(program.Id.ToString()).SendAsync("TabletShowDraw", null, null, null, null);
        //        return;
        //    }

        //    var employee = db.LKD_Employees.Any(x => x.ProgramId == program.Id && x.Turn == turnNo && x.RewardId == reward.Id && x.RewardVisibled == true);
        //    if (employee)
        //    {
        //        await Clients.Client(connectionId).SendAsync("Exception", "Giải đã được quay vui lòng chọn giải khác");
        //        await Clients.Group(program.Id.ToString()).SendAsync("TabletShowDraw", null, null, null, null);
        //        return;
        //    }

        //    await Clients.Group(program.Id.ToString()).SendAsync("TabletShowDraw", reward.Id, reward.Name, turnNo, reward.Turn);
        //}

        //public async Task TabletDrawing(string id, int rewardid, int turn, string connectionId)
        //{
        //    if (turn == 0)
        //    {
        //        await Clients.Client(connectionId).SendAsync("Exception", "Lượt quay không hợp lệ");
        //    }
        //    var program = db.LKD_Programs.FirstOrDefault(x => x.TabletId.ToString() == id);
        //    if (program == null)
        //    {
        //        await Clients.Client(connectionId).SendAsync("Exception", "Không tìm thấy chương trình");
        //        return;
        //    }

        //    var reward = db.LKD_Rewards.FirstOrDefault(x => x.Id == rewardid);
        //    if (reward == null)
        //    {
        //        await Clients.Group(program.Id.ToString()).SendAsync("TabletShowDraw", null, null, null, null);
        //        return;
        //    }

        //    if (reward.Turn < turn)
        //    {
        //        await Clients.Client(connectionId).SendAsync("Exception", "Lượt quay được chọn lớn hơn số lượng lượt tối đa");
        //        return;
        //    }

        //    var rewardExist = db.LKD_Employees.Any(x => x.ProgramId == program.Id && x.Turn == turn && x.RewardId == reward.Id && x.RewardVisibled == true);
        //    if (rewardExist)
        //    {
        //        await Clients.Client(connectionId).SendAsync("Exception", "Giải đã được quay vui lòng chọn giải khác");
        //        return;
        //    }

        //    await Clients.Group(program.Id.ToString()).SendAsync("DisplayDrawing", program.Id, reward.Id, turn);

        //    int turnCount = reward.PersonOfTurn;
        //    var lkdEmployee = db.LKD_Employees.Where(x => x.ProgramId == program.Id).ToList();
        //    for (int no = 1; no <= turnCount; no++)
        //    {
        //        var employee = lkdEmployee.FirstOrDefault(x => x.ProgramId == program.Id && x.RewardId == reward.Id && x.Turn == turn && x.No == no);
        //        if (employee != null)
        //        {
        //            employee.RewardVisibled = false;
        //          //  Thread.Sleep(20);
        //            continue;
        //        }
        //        var employeeCount = lkdEmployee.Count(x => x.ProgramId == program.Id && x.RewardId == null && x.Turn == null);
        //        Random rd = new Random();
        //        var luckNumber = rd.Next(0, employeeCount);
        //        var luckyEmployee = lkdEmployee.Where(x => x.ProgramId == program.Id && x.RewardId == null && x.Turn == null).OrderBy(x => x.EmpId).Skip(luckNumber).FirstOrDefault();
        //        if (luckyEmployee == null)
        //        {
        //            no--;
        //        }
        //        luckyEmployee.No = no;
        //        luckyEmployee.RewardId = reward.Id;
        //        luckyEmployee.Turn = turn;
        //        luckyEmployee.RewardVisibled = false;
        //        //Thread.Sleep(20);
        //    }
        //    await db.SaveChangesAsync();
        //    await Clients.Group(program.Id.ToString()).SendAsync("TabletDrawFinish");
        //}

        //public async Task TabletStopDrawing(string id, int rewardid, int turn, string connectionId)
        //{
        //    if (turn == 0)
        //    {
        //        await Clients.Client(connectionId).SendAsync("Exception", "Lượt quay không hợp lệ");
        //    }
        //    var program = db.LKD_Programs.FirstOrDefault(x => x.TabletId.ToString() == id);
        //    if (program == null)
        //    {
        //        await Clients.Client(connectionId).SendAsync("Exception", "Không tìm thấy chương trình");
        //        return;
        //    }

        //    var reward = db.LKD_Rewards.FirstOrDefault(x => x.Id == rewardid);
        //    if (reward == null)
        //    {
        //        await Clients.Group(program.Id.ToString()).SendAsync("TabletShowDraw", null, null, null, null);
        //        return;
        //    }

        //    if (reward.Turn < turn)
        //    {
        //        await Clients.Client(connectionId).SendAsync("Exception", "Lượt quay được chọn lớn hơn số lượng lượt tối đa");
        //        return;
        //    }

        //    var rewardExist = db.LKD_Employees.Any(x => x.ProgramId == program.Id && x.Turn == turn && x.RewardId == reward.Id && x.RewardVisibled == true);
        //    if (rewardExist)
        //    {
        //        await Clients.Client(connectionId).SendAsync("Exception", "Giải đã được quay vui lòng chọn giải khác");
        //        return;
        //    }

        //    var employees = db.LKD_Employees.Where(x => x.ProgramId == program.Id && x.RewardId == reward.Id && x.Turn == turn).OrderBy(x => x.EmpId).Take(reward.PersonOfTurn).ToList();
        //    foreach (var item in employees)
        //    {
        //        item.RewardVisibled = true;
        //    }
        //    await db.SaveChangesAsync();

        //    await Clients.Group(program.Id.ToString()).SendAsync("DisplayLuckyEmployee", program.Id, reward.Id, turn);
        //    Thread.Sleep(reward.PersonOfTurn * 100);
        //    await Clients.Group(program.Id.ToString()).SendAsync("TabletGoToNext");
        //}

        //public async Task TabletCallNextTurn(string id, int rewardid, int turn, string connectionId)
        //{
        //    var program = db.LKD_Programs.FirstOrDefault(x => x.TabletId.ToString() == id);
        //    if (program == null)
        //    {
        //        await Clients.Client(connectionId).SendAsync("Exception", "Không tìm thấy chương trình");
        //        return;
        //    }

        //    var reward = db.LKD_Rewards.FirstOrDefault(x => x.Id == rewardid);
        //    if (reward == null)
        //    {
        //        await Clients.Group(program.Id.ToString()).SendAsync("TabletShowDraw", null, null, null, null);
        //        return;
        //    }

        //    if (reward.Turn < turn)
        //    {
        //        await Clients.Client(connectionId).SendAsync("Exception", "Lượt quay được chọn lớn hơn số lượng lượt tối đa");
        //        return;
        //    }
        //    await Clients.Group(program.Id.ToString()).SendAsync("ControlGotoNextTurn", turn + 1);
        //    var page = db.LKD_Mains.FirstOrDefault(x => x.ProgramId == program.Id && x.RewardId == reward.Id)?.Id ?? 0;

        //    Thread.Sleep(2000);
        //    await ControlChooseTurn(program.ControlId.ToString(), page, turn + 1, "");
        //}

        //public async Task SetClientGroupId(string id, string connectionId)
        //{
        //    var program = db.LKD_Programs.FirstOrDefault(x => x.TabletId.ToString() == id || x.ControlId.ToString() == id || x.DisplayId.ToString() == id);
        //    if (program == null)
        //    {
        //        await Clients.Client(connectionId).SendAsync("Exception", "Không tìm thấy chương trình");
        //        return;
        //    }
        //    await Groups.AddToGroupAsync(connectionId, program.Id.ToString());
        //}


    }
}
