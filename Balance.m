con = database('d60648171','u70715654','620f98','com.mysql.jdbc.Driver','jdbc:mysql://mysql.netfirms.com/d60648171');
%NET.addAssembly('C:\Program Files (x86)\Microsoft SQL Server Compact Edition\v3.5\desktop\System.Data.SqlServerCe.dll');
%con = System.Data.SqlServerCe.SqlCeConnection('Datasource=C:\work\balanceproj\Balance\bin\Release\balance.sdf;password=');
conn = DBConnection(0, con);
setdbprefs('DataReturnFormat','cellarray');
items_ = Items.GetItems(conn);
[s,v] = listdlg('PromptString','Select Item(s):',...
    'SelectionMode','multiple',...
    'ListString',items_);
if(v == 0 || isempty(s))
    return;
end;
items = cell(length(s),1);
for i = 1:length(s)
    items(i) = items_(s(i));
end;

choice = questdlg('Please choose plot type','Plot Type','Sales','Price','Sales');

if(strcmp(choice,'Price'))
    u = ItemPrice(items, 60);    
    PlotItemPrice(u, conn);
elseif(strcmp(choice,'Sales'))
    u = ItemSales(items, 60);
    PlotItemSales(u,conn);
end;
conn.close();