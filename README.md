# Remote Config Tool 

A simple remote config tool integrated into project settings. 

![Screenshot 2023-11-27 at 16 26 48](https://github.com/Ale1/remote-config/assets/4612160/d80ff928-1cfe-4034-946c-1a3cd1977026)





Notes:

+ compatible with NPM-style packages (should work fine with a private verdaccio server too)
+ Example packages: localisation and purchases.  
  + Localisation has Remote config package as a hard dependency (classic setup).
  + while purchases shows setup where Remote Config package is optional.
+ Remote Config is saved twice:  
  + a Remote instance (not truly remote, just caches what was last fetched from remote)
  + and a local instance. Intended workflow: You can push the local instance with changes unto the remote instance, and then (in theory) upload the remote one.  The same flow goes in reverse for pulling changes from the server.
+ To avoid re-inventing the wheel and creating a whole new editor inspector from scratch (fun, but time-counsuming) I used scriptable Objects to leverage their pre-existing inspector editors and relatively easy integration with Project Settings.
+ However, half-way through I regretted this choice, and wish I had done a pure c#+json implementation, caching everything in serialized jsons and only using unity for the visual editor window.
+ The end results is a bit of a clunky mix of both approaches.  Pls it requires using config files in a resources folder, which is super duper annoying -even through its the recommended "unity way".
+ Anyway, given I didnt have a lot of time due to busy schedule, I consider it a decent job, specially since everything (except serializable dictionary) was made from scratch.  
+ Note that I am missing a few trivial integrations of the package so the editor features also work seamlessy in runtime. But nothing architecturally needs to change to make this happen, its just a few wires here and there. 
+ Also would have liked to add some unity tests (and I believe a Remote Config system is ideal for unit testing!) but alas time constraints.   
