using MilitiaDuty.Models.Options;
using MilitiaDuty.Models.Rules;

namespace MilitiaDuty.Data
{
    public static class Seeder
    {
        public static async Task Seed(MilitiaOptions options)
        {
            var context = new MilitiaContext();

            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            SeedMilitias(context, options);
            SeedDutyDates(context);
            SeedRules(context);
            SeedMissions(context);
            SeedTasks(context);
            SeedShifts(context);
            SeedTaskRules(context);

            await context.SaveChangesAsync();
        }

        private static void SeedMilitias(MilitiaContext context, MilitiaOptions options)
        {
            context.Militias.AddRange(
                new() { Id = 1, Name = "Hiếu", DutyDateScore = (ushort)(options.OnDutyRate * 2) },
                new() { Id = 2, Name = "Danh nhỏ", DutyDateScore = (ushort)(options.OnDutyRate * 3) },
                new() { Id = 3, Name = "Minh", DutyDateScore = (ushort)(options.OnDutyRate * 2) },
                new() { Id = 4, Name = "Châu", DutyDateScore = options.OnDutyRate },
                new() { Id = 5, Name = "Thắng", DutyDateScore = options.OnDutyRate },
                new() { Id = 6, Name = "Đạt", DutyDateScore = options.OnDutyRate },
                new() { Id = 7, Name = "Thịnh", DutyDateScore = options.OnDutyRate },
                new() { Id = 8, Name = "Long", DutyDateScore = (ushort)(options.OnDutyRate * 4) },
                new() { Id = 9, Name = "Khôi", DutyDateScore = (ushort)(options.OnDutyRate * 2) },
                new() { Id = 10, Name = "Hưng", DutyDateScore = (ushort)(options.OnDutyRate * 2) },
                new() { Id = 11, Name = "Phước", DutyDateScore = (ushort)(options.OnDutyRate * 2) },
                new() { Id = 12, Name = "Phương", DutyDateScore = (ushort)(options.OnDutyRate * 2) },
                new() { Id = 13, Name = "Kiệt", DutyDateScore = (ushort)(options.OnDutyRate * 3) },
                new() { Id = 14, Name = "Lợi", DutyDateScore = (ushort)(options.OnDutyRate * 5) },
                new() { Id = 15, Name = "Thạch", DutyDateScore = (ushort)(options.OnDutyRate * 2) },
                new() { Id = 16, Name = "Thành", DutyDateScore = (ushort)(options.OnDutyRate * 5) },
                new() { Id = 17, Name = "Lộc", DutyDateScore = (ushort)(options.OnDutyRate * 4) },
                new() { Id = 18, Name = "Phúc" },
                new() { Id = 19, Name = "Danh lớn", DutyDateScore = (ushort)(options.OnDutyRate * 3) },
                new() { Id = 20, Name = "An", DutyDateScore = (ushort)(options.OnDutyRate * 3) },
                new() { Id = 21, Name = "Tùng", DutyDateScore = (ushort)(options.OnDutyRate * 4) },
                new() { Id = 22, Name = "Nghĩa", DutyDateScore = (ushort)(options.OnDutyRate * 3) },
                new() { Id = 23, Name = "Dũng", DutyDateScore = (ushort)(options.OnDutyRate * 3) },
                new() { Id = 24, Name = "Đại" }
            );
        }

        private static void SeedDutyDates(MilitiaContext context)
        {
            context.DutyDates.AddRange(
                new() { Id = "20240207", Date = new DateTime(2024, 2, 7), IsFullDutyDate = true },
                new() { Id = "20240208", Date = new DateTime(2024, 2, 8), IsFullDutyDate = true },
                new() { Id = "20240209", Date = new DateTime(2024, 2, 9), IsFullDutyDate = true },
                new() { Id = "20240210", Date = new DateTime(2024, 2, 10), IsFullDutyDate = true },
                new() { Id = "20240211", Date = new DateTime(2024, 2, 11), IsFullDutyDate = true },
                new() { Id = "20240212", Date = new DateTime(2024, 2, 12), IsFullDutyDate = true },
                new() { Id = "20240213", Date = new DateTime(2024, 2, 13), IsFullDutyDate = true },
                new() { Id = "20240214", Date = new DateTime(2024, 2, 14), IsFullDutyDate = true },
                new() { Id = "20240215", Date = new DateTime(2024, 2, 15) },
                new() { Id = "20240216", Date = new DateTime(2024, 2, 16) },
                new() { Id = "20240217", Date = new DateTime(2024, 2, 17) },
                new() { Id = "20240218", Date = new DateTime(2024, 2, 18) },
                new() { Id = "20240219", Date = new DateTime(2024, 2, 19) },
                new() { Id = "20240220", Date = new DateTime(2024, 2, 20) },
                new() { Id = "20240221", Date = new DateTime(2024, 2, 21) },
                new() { Id = "20240222", Date = new DateTime(2024, 2, 22) },
                new() { Id = "20240223", Date = new DateTime(2024, 2, 23) },
                new() { Id = "20240224", Date = new DateTime(2024, 2, 24) },
                new() { Id = "20240225", Date = new DateTime(2024, 2, 25) }
            );

            context.MilitiaDutyDates.AddRange(
                new() { DutyDateId = "20240215", MilitiaId = 3 },
                new() { DutyDateId = "20240215", MilitiaId = 7 },
                new() { DutyDateId = "20240215", MilitiaId = 8 },
                new() { DutyDateId = "20240215", MilitiaId = 9 },
                new() { DutyDateId = "20240215", MilitiaId = 10 },
                new() { DutyDateId = "20240215", MilitiaId = 11 },
                new() { DutyDateId = "20240215", MilitiaId = 12 },
                new() { DutyDateId = "20240215", MilitiaId = 13 },
                new() { DutyDateId = "20240215", MilitiaId = 15 },
                new() { DutyDateId = "20240215", MilitiaId = 17 },
                new() { DutyDateId = "20240215", MilitiaId = 18 },
                new() { DutyDateId = "20240215", MilitiaId = 19 },
                new() { DutyDateId = "20240215", MilitiaId = 20 },
                new() { DutyDateId = "20240215", MilitiaId = 21 }
            );

            context.MilitiaDutyDates.AddRange(
                new() { DutyDateId = "20240216", MilitiaId = 1 },
                new() { DutyDateId = "20240216", MilitiaId = 2 },
                new() { DutyDateId = "20240216", MilitiaId = 4 },
                new() { DutyDateId = "20240216", MilitiaId = 5 },
                new() { DutyDateId = "20240216", MilitiaId = 6 },
                new() { DutyDateId = "20240216", MilitiaId = 7 },
                new() { DutyDateId = "20240216", MilitiaId = 8 },
                new() { DutyDateId = "20240216", MilitiaId = 17 },
                new() { DutyDateId = "20240216", MilitiaId = 18 },
                new() { DutyDateId = "20240216", MilitiaId = 19 },
                new() { DutyDateId = "20240216", MilitiaId = 20 },
                new() { DutyDateId = "20240216", MilitiaId = 21 },
                new() { DutyDateId = "20240216", MilitiaId = 22 },
                new() { DutyDateId = "20240216", MilitiaId = 23 },
                new() { DutyDateId = "20240216", MilitiaId = 24 }
            );

            context.MilitiaDutyDates.AddRange(
                new() { DutyDateId = "20240217", MilitiaId = 2 },
                new() { DutyDateId = "20240217", MilitiaId = 4 },
                new() { DutyDateId = "20240217", MilitiaId = 5 },
                new() { DutyDateId = "20240217", MilitiaId = 6 },
                new() { DutyDateId = "20240217", MilitiaId = 8 },
                new() { DutyDateId = "20240217", MilitiaId = 9 },
                new() { DutyDateId = "20240217", MilitiaId = 10 },
                new() { DutyDateId = "20240217", MilitiaId = 11 },
                new() { DutyDateId = "20240217", MilitiaId = 12 },
                new() { DutyDateId = "20240217", MilitiaId = 13 },
                new() { DutyDateId = "20240217", MilitiaId = 21 },
                new() { DutyDateId = "20240217", MilitiaId = 22 },
                new() { DutyDateId = "20240217", MilitiaId = 23 },
                new() { DutyDateId = "20240217", MilitiaId = 24 }
            );

            context.MilitiaDutyDates.AddRange(
                new() { DutyDateId = "20240218", MilitiaId = 1 },
                new() { DutyDateId = "20240218", MilitiaId = 8 },
                new() { DutyDateId = "20240218", MilitiaId = 9 },
                new() { DutyDateId = "20240218", MilitiaId = 10 },
                new() { DutyDateId = "20240218", MilitiaId = 11 },
                new() { DutyDateId = "20240218", MilitiaId = 12 },
                new() { DutyDateId = "20240218", MilitiaId = 13 },
                new() { DutyDateId = "20240218", MilitiaId = 17 },
                new() { DutyDateId = "20240218", MilitiaId = 18 },
                new() { DutyDateId = "20240218", MilitiaId = 19 },
                new() { DutyDateId = "20240218", MilitiaId = 20 },
                new() { DutyDateId = "20240218", MilitiaId = 21 },
                new() { DutyDateId = "20240218", MilitiaId = 22 },
                new() { DutyDateId = "20240218", MilitiaId = 23 },
                new() { DutyDateId = "20240218", MilitiaId = 24 }
            );

            context.MilitiaDutyDates.AddRange(
                new() { DutyDateId = "20240219", MilitiaId = 1 },
                new() { DutyDateId = "20240219", MilitiaId = 2 },
                new() { DutyDateId = "20240219", MilitiaId = 3 },
                new() { DutyDateId = "20240219", MilitiaId = 4 },
                new() { DutyDateId = "20240219", MilitiaId = 5 },
                new() { DutyDateId = "20240219", MilitiaId = 6 },
                new() { DutyDateId = "20240219", MilitiaId = 7 },
                new() { DutyDateId = "20240219", MilitiaId = 8 },
                new() { DutyDateId = "20240219", MilitiaId = 15 },
                new() { DutyDateId = "20240219", MilitiaId = 17 },
                new() { DutyDateId = "20240219", MilitiaId = 18 },
                new() { DutyDateId = "20240219", MilitiaId = 19 },
                new() { DutyDateId = "20240219", MilitiaId = 20 },
                new() { DutyDateId = "20240219", MilitiaId = 21 },
                new() { DutyDateId = "20240219", MilitiaId = 22 },
                new() { DutyDateId = "20240219", MilitiaId = 23 },
                new() { DutyDateId = "20240219", MilitiaId = 24 }
            );

            context.MilitiaDutyDates.AddRange(
                new() { DutyDateId = "20240220", MilitiaId = 1 },
                new() { DutyDateId = "20240220", MilitiaId = 2 },
                new() { DutyDateId = "20240220", MilitiaId = 3 },
                new() { DutyDateId = "20240220", MilitiaId = 4 },
                new() { DutyDateId = "20240220", MilitiaId = 5 },
                new() { DutyDateId = "20240220", MilitiaId = 6 },
                new() { DutyDateId = "20240220", MilitiaId = 7 },
                new() { DutyDateId = "20240220", MilitiaId = 8 },
                new() { DutyDateId = "20240220", MilitiaId = 9 },
                new() { DutyDateId = "20240220", MilitiaId = 10 },
                new() { DutyDateId = "20240220", MilitiaId = 11 },
                new() { DutyDateId = "20240220", MilitiaId = 12 },
                new() { DutyDateId = "20240220", MilitiaId = 13 },
                new() { DutyDateId = "20240220", MilitiaId = 14 },
                new() { DutyDateId = "20240220", MilitiaId = 15 },
                new() { DutyDateId = "20240220", MilitiaId = 16 },
                new() { DutyDateId = "20240220", MilitiaId = 21 }
            );

            context.MilitiaDutyDates.AddRange(
                new() { DutyDateId = "20240221", MilitiaId = 3 },
                new() { DutyDateId = "20240221", MilitiaId = 7 },
                new() { DutyDateId = "20240221", MilitiaId = 9 },
                new() { DutyDateId = "20240221", MilitiaId = 10 },
                new() { DutyDateId = "20240221", MilitiaId = 11 },
                new() { DutyDateId = "20240221", MilitiaId = 12 },
                new() { DutyDateId = "20240221", MilitiaId = 13 },
                new() { DutyDateId = "20240221", MilitiaId = 14 },
                new() { DutyDateId = "20240221", MilitiaId = 15 },
                new() { DutyDateId = "20240221", MilitiaId = 16 },
                new() { DutyDateId = "20240221", MilitiaId = 17 },
                new() { DutyDateId = "20240221", MilitiaId = 18 },
                new() { DutyDateId = "20240221", MilitiaId = 19 },
                new() { DutyDateId = "20240221", MilitiaId = 20 },
                new() { DutyDateId = "20240221", MilitiaId = 21 },
                new() { DutyDateId = "20240221", MilitiaId = 22 },
                new() { DutyDateId = "20240221", MilitiaId = 23 },
                new() { DutyDateId = "20240221", MilitiaId = 24 }
            );

            context.MilitiaDutyDates.AddRange(
                new() { DutyDateId = "20240222", MilitiaId = 1 },
                new() { DutyDateId = "20240222", MilitiaId = 2 },
                new() { DutyDateId = "20240222", MilitiaId = 3 },
                new() { DutyDateId = "20240222", MilitiaId = 4 },
                new() { DutyDateId = "20240222", MilitiaId = 5 },
                new() { DutyDateId = "20240222", MilitiaId = 6 },
                new() { DutyDateId = "20240222", MilitiaId = 8 },
                new() { DutyDateId = "20240222", MilitiaId = 15 },
                new() { DutyDateId = "20240222", MilitiaId = 17 },
                new() { DutyDateId = "20240222", MilitiaId = 18 },
                new() { DutyDateId = "20240222", MilitiaId = 19 },
                new() { DutyDateId = "20240222", MilitiaId = 20 },
                new() { DutyDateId = "20240222", MilitiaId = 21 },
                new() { DutyDateId = "20240222", MilitiaId = 22 },
                new() { DutyDateId = "20240222", MilitiaId = 23 },
                new() { DutyDateId = "20240222", MilitiaId = 24 }
            );

            context.MilitiaDutyDates.AddRange(
                new() { DutyDateId = "20240223", MilitiaId = 1 },
                new() { DutyDateId = "20240223", MilitiaId = 2 },
                new() { DutyDateId = "20240223", MilitiaId = 3 },
                new() { DutyDateId = "20240223", MilitiaId = 4 },
                new() { DutyDateId = "20240223", MilitiaId = 5 },
                new() { DutyDateId = "20240223", MilitiaId = 6 },
                new() { DutyDateId = "20240223", MilitiaId = 7 },
                new() { DutyDateId = "20240223", MilitiaId = 8 },
                new() { DutyDateId = "20240223", MilitiaId = 9 },
                new() { DutyDateId = "20240223", MilitiaId = 10 },
                new() { DutyDateId = "20240223", MilitiaId = 11 },
                new() { DutyDateId = "20240223", MilitiaId = 12 },
                new() { DutyDateId = "20240223", MilitiaId = 13 },
                new() { DutyDateId = "20240223", MilitiaId = 14 },
                new() { DutyDateId = "20240223", MilitiaId = 15 },
                new() { DutyDateId = "20240223", MilitiaId = 16 }
            );

            context.MilitiaDutyDates.AddRange(
                new() { DutyDateId = "20240224", MilitiaId = 7 },
                new() { DutyDateId = "20240224", MilitiaId = 9 },
                new() { DutyDateId = "20240224", MilitiaId = 10 },
                new() { DutyDateId = "20240224", MilitiaId = 11 },
                new() { DutyDateId = "20240224", MilitiaId = 12 },
                new() { DutyDateId = "20240224", MilitiaId = 13 },
                new() { DutyDateId = "20240224", MilitiaId = 14 },
                new() { DutyDateId = "20240224", MilitiaId = 16 },
                new() { DutyDateId = "20240224", MilitiaId = 17 },
                new() { DutyDateId = "20240224", MilitiaId = 18 },
                new() { DutyDateId = "20240224", MilitiaId = 19 },
                new() { DutyDateId = "20240224", MilitiaId = 20 },
                new() { DutyDateId = "20240224", MilitiaId = 21 },
                new() { DutyDateId = "20240224", MilitiaId = 22 },
                new() { DutyDateId = "20240224", MilitiaId = 23 },
                new() { DutyDateId = "20240224", MilitiaId = 24 }
            );

            context.MilitiaDutyDates.AddRange(
                new() { DutyDateId = "20240225", MilitiaId = 3 },
                new() { DutyDateId = "20240225", MilitiaId = 7 },
                new() { DutyDateId = "20240225", MilitiaId = 9 },
                new() { DutyDateId = "20240225", MilitiaId = 10 },
                new() { DutyDateId = "20240225", MilitiaId = 11 },
                new() { DutyDateId = "20240225", MilitiaId = 12 },
                new() { DutyDateId = "20240225", MilitiaId = 13 },
                new() { DutyDateId = "20240225", MilitiaId = 14 },
                new() { DutyDateId = "20240225", MilitiaId = 15 },
                new() { DutyDateId = "20240225", MilitiaId = 16 },
                new() { DutyDateId = "20240225", MilitiaId = 17 },
                new() { DutyDateId = "20240225", MilitiaId = 18 },
                new() { DutyDateId = "20240225", MilitiaId = 19 },
                new() { DutyDateId = "20240225", MilitiaId = 20 },
                new() { DutyDateId = "20240225", MilitiaId = 21 },
                new() { DutyDateId = "20240225", MilitiaId = 22 },
                new() { DutyDateId = "20240225", MilitiaId = 23 },
                new() { DutyDateId = "20240225", MilitiaId = 24 }
            );
        }

        private static void SeedRules(MilitiaContext context)
        {
            context.Rules.AddRange(
                new()
                {
                    Id = 1,
                    StartDate = new DateTime(2024, 1, 1),
                    Type = RuleType.WeeklyDutyOnly,
                    Weekdays = new[] { DayOfWeek.Tuesday, DayOfWeek.Thursday, DayOfWeek.Saturday },
                    Description = "Phúc chỉ trực 3-5-7"
                },
                new()
                {
                    Id = 2,
                    StartDate = new DateTime(2024, 1, 1),
                    Type = RuleType.NoAllOff,
                    Description = "a trưởng và a phó ko dc off cùng ngày"
                },
                new()
                {
                    Id = 3,
                    StartDate = new DateTime(2024, 2, 15),
                    Type = RuleType.DateOff,
                    Description = "Khôi off ngày 15-2"
                },
                new()
                {
                    Id = 4,
                    StartDate = new DateTime(2024, 1, 1),
                    Type = RuleType.OnDutyRate,
                    NumberValue = 1,
                    Description = "trực 1 nghỉ 1"
                }
            );

            context.MilitiaRules.AddRange(
                new() { RuleId = 1, MilitiaId = 18 },
                new() { RuleId = 2, MilitiaId = 5 },
                new() { RuleId = 2, MilitiaId = 19 },
                new() { RuleId = 3, MilitiaId = 9 },
                new() { RuleId = 4, MilitiaId = 5 },
                new() { RuleId = 4, MilitiaId = 19 }
            );
        }

        private static void SeedMissions(MilitiaContext context)
        {
            context.Missions.AddRange(
                new() { Id = 1, Name = "Gác cổng ủy ban" },
                new() { Id = 2, Name = "Gác cổng trụ sở mới" }
            );
        }

        private static void SeedTasks(MilitiaContext context)
        {
            context.Tasks.AddRange(
                new() { Id = 1, MissionId = 1, StartMinute = 1140, EndMinute = 1230, MilitiaNumber = 2 },
                new() { Id = 2, MissionId = 1, StartMinute = 1230, EndMinute = 1320, MilitiaNumber = 2 },
                new() { Id = 3, MissionId = 1, StartMinute = 1320, EndMinute = 1800, MilitiaNumber = 2 },
                new() { Id = 4, MissionId = 2, StartMinute = 1140, EndMinute = 1230, MilitiaNumber = 2 },
                new() { Id = 5, MissionId = 2, StartMinute = 1230, EndMinute = 1320, MilitiaNumber = 2 },
                new() { Id = 6, MissionId = 2, StartMinute = 1320, EndMinute = 1800, MilitiaNumber = 2 }
            );
        }

        private static void SeedShifts(MilitiaContext context)
        {
            context.Shifts.AddRange(
                new() { Id = 1, DutyDateId = "20240207", TaskId = 1, MilitiaId = 6 },
                new() { Id = 2, DutyDateId = "20240207", TaskId = 1, MilitiaId = 8 },
                new() { Id = 3, DutyDateId = "20240207", TaskId = 2, MilitiaId = 7 },
                new() { Id = 4, DutyDateId = "20240207", TaskId = 2, MilitiaId = 15 },
                new() { Id = 5, DutyDateId = "20240207", TaskId = 3, MilitiaId = 3 },
                new() { Id = 6, DutyDateId = "20240207", TaskId = 3, MilitiaId = 1 },
                new() { Id = 7, DutyDateId = "20240208", TaskId = 1, MilitiaId = 2 },
                new() { Id = 8, DutyDateId = "20240208", TaskId = 1, MilitiaId = 4 },
                new() { Id = 9, DutyDateId = "20240208", TaskId = 2, MilitiaId = 10 },
                new() { Id = 10, DutyDateId = "20240208", TaskId = 2, MilitiaId = 17 },
                new() { Id = 11, DutyDateId = "20240208", TaskId = 3, MilitiaId = 12 },
                new() { Id = 12, DutyDateId = "20240208", TaskId = 3, MilitiaId = 20 },
                new() { Id = 13, DutyDateId = "20240209", TaskId = 1, MilitiaId = 1 },
                new() { Id = 14, DutyDateId = "20240209", TaskId = 1, MilitiaId = 19 },
                new() { Id = 15, DutyDateId = "20240209", TaskId = 2, MilitiaId = 18 },
                new() { Id = 16, DutyDateId = "20240209", TaskId = 2, MilitiaId = 6 },
                new() { Id = 17, DutyDateId = "20240209", TaskId = 3, MilitiaId = 7 },
                new() { Id = 18, DutyDateId = "20240209", TaskId = 3, MilitiaId = 21 },
                new() { Id = 19, DutyDateId = "20240207", TaskId = 4, MilitiaId = 13 },
                new() { Id = 20, DutyDateId = "20240207", TaskId = 4, MilitiaId = 19 },
                new() { Id = 21, DutyDateId = "20240207", TaskId = 5, MilitiaId = 18 },
                new() { Id = 22, DutyDateId = "20240207", TaskId = 5, MilitiaId = 11 },
                new() { Id = 23, DutyDateId = "20240207", TaskId = 6, MilitiaId = 17 },
                new() { Id = 24, DutyDateId = "20240207", TaskId = 6, MilitiaId = 12 },
                new() { Id = 25, DutyDateId = "20240208", TaskId = 4, MilitiaId = 15 },
                new() { Id = 26, DutyDateId = "20240208", TaskId = 4, MilitiaId = 8 },
                new() { Id = 27, DutyDateId = "20240208", TaskId = 5, MilitiaId = 3 },
                new() { Id = 28, DutyDateId = "20240208", TaskId = 5, MilitiaId = 13 },
                new() { Id = 29, DutyDateId = "20240208", TaskId = 6, MilitiaId = 14 },
                new() { Id = 30, DutyDateId = "20240208", TaskId = 6, MilitiaId = 16 },
                new() { Id = 31, DutyDateId = "20240209", TaskId = 4, MilitiaId = 23 },
                new() { Id = 32, DutyDateId = "20240209", TaskId = 4, MilitiaId = 5 },
                new() { Id = 33, DutyDateId = "20240209", TaskId = 5, MilitiaId = 11 },
                new() { Id = 34, DutyDateId = "20240209", TaskId = 5, MilitiaId = 9 },
                new() { Id = 35, DutyDateId = "20240209", TaskId = 6, MilitiaId = 22 },
                new() { Id = 36, DutyDateId = "20240209", TaskId = 6, MilitiaId = 24 }
            );
        }

        private static void SeedTaskRules(MilitiaContext context)
        {
            context.Rules.AddRange(
                new()
                {
                    Id = 5,
                    StartDate = new DateTime(2024, 1, 1),
                    Type = RuleType.IncludeTasks,
                    Description = "đi làm hay vô trễ nên coi cổng ca khuya",
                },
                new()
                {
                    Id = 6,
                    StartDate = new DateTime(2024, 1, 1),
                    Type = RuleType.IncludeTasks,
                    Description = "đi làm nên ko coi cổng ca 7h dc",
                },
                new()
                {
                    Id = 7,
                    StartDate = new DateTime(2024, 1, 1),
                    Type = RuleType.TaskImmune,
                    Description = "ko cần coi cổng",
                },
                new()
                {
                    Id = 8,
                    StartDate = new DateTime(2024, 1, 1),
                    Type = RuleType.ExcludeTasks,
                    Description = "ko ngủ chốt khuya"
                }
            );

            context.MilitiaRules.AddRange(
                new() { RuleId = 5, MilitiaId = 3 },
                new() { RuleId = 5, MilitiaId = 12 },
                new() { RuleId = 5, MilitiaId = 14 },
                new() { RuleId = 5, MilitiaId = 16 },
                new() { RuleId = 5, MilitiaId = 18 },
                new() { RuleId = 6, MilitiaId = 22 },
                new() { RuleId = 7, MilitiaId = 5 },
                new() { RuleId = 8, MilitiaId = 19 }
            );

            context.TaskRules.AddRange(
                new() { RuleId = 5, TaskId = 3 },
                new() { RuleId = 5, TaskId = 6 },
                new() { RuleId = 6, TaskId = 2 },
                new() { RuleId = 6, TaskId = 3 },
                new() { RuleId = 6, TaskId = 5 },
                new() { RuleId = 6, TaskId = 6 },
                new() { RuleId = 8, TaskId = 3 },
                new() { RuleId = 8, TaskId = 6 }
            );
        }
    }
}
