function [objectList] = recSearch(objectEnum, currentObject)
objectList = [];
for i = 0:currentObject.Children.Count - 1
    child = currentObject.Children.Item(int32(i));
    if strcmp(child.ClassType, objectEnum)
       objectList = [objectList child];
    end
    objectList = [objectList recSearch(objectEnum, child)];
end
    
    
  
  

       