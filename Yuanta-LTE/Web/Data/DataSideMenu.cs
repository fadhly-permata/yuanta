using Web.Models;

namespace Web.Data;

static public class DataSideMenu
{
    static public List<AdminLteMenuModel> GetMenus()
    {
        return new List<AdminLteMenuModel>(){
            new AdminLteMenuModel{
                Icon = "fas fa-tachometer-alt",
                Text = "Parent-Child",
                Childs = new List<AdminLteMenuModel>(){
                    new AdminLteMenuModel{
                        Text = "Child 1.1",
                        Path = "/testpage"
                    },
                    new AdminLteMenuModel{
                        Text = "Child 1.2",
                        Path = "/testpage"
                    },
                    new AdminLteMenuModel{
                        Text = "Child 1.3",
                        Path = "/testpage"
                    }
                }
            },
            new AdminLteMenuModel{
                Icon = "fas fa-chart-pie",
                Text = "Multi Level",
                Childs = new List<AdminLteMenuModel>(){
                    new AdminLteMenuModel{
                        Text = "Child 2.1",
                        Path = "/testpage"
                    },
                    new AdminLteMenuModel{
                        Text = "Child 2.2",
                        Path = "/testpage"
                    },
                    new AdminLteMenuModel{
                        Text = "Child 2.3",
                        Childs = new List<AdminLteMenuModel>() {
                            new AdminLteMenuModel{
                                Text = "Child 2.3.1",
                                Path = "/testpage"
                            },
                            new AdminLteMenuModel{
                                Text = "Child 2.3.2",
                                Path = "/testpage"
                            }
                        }
                    }
                }
            },
            new AdminLteMenuModel{
                Icon = "far fa-calendar-alt",
                Text = "Calendar",
                Path = "/testpage/calendar"
            },
        };
    }
}
