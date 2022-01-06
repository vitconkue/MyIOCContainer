# MyIOCContainer
A simple Ioc container implementation 

# Currently done: 
  ## Register (transient, one time using) an abstraction with an implementation and get it the real object out for using 
    - Nice thing: If there is an abstraction type in the constructor, the container wil try to find the concrete registered implemetation type, if it can't find it, an error is throw
    - If there are implemetatation types in the constructor, the container will try to fill it too. 
# What am I going to add ? 
    - Register singleton objects 
    - Register with user-chosen constructor parameters
    - Register a interceptor to an abstraction - implementation pair.  
    
