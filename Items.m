classdef Items
    %UNTITLED2 Summary of this class goes here
    %   Detailed explanation goes here
    
    properties
    end
    
    methods (Static=true)
        function [items] = GetItems(conn)
            sql = 'select distinct item from txn';
            curs = exec(conn,sql);
            curs = fetch(conn,curs);
            items = curs.Data(:,1);          
        end;
    end    
end

