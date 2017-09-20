# FORK!

> :warning: This is a fork of original [nrobot-server](https://github.com/claytonneal/nrobot-server). I __strongly__ recommend to use original library (and [nuget package](https://www.nuget.org/packages/NRobotServer/)) created by [Clayton Neal](https://github.com/claytonneal) to everyone.

Main purpose of this fork:
- removal of `IsAdministrator` check as it may (unnecessarily) interfere with CI/CD environment
- removal of executable, effectively leaving just a library which could be integrated into app or service as necessary
- abstracted/simplified configuration loading  

# nrobot-server
.Net Framework Integration for Robot Framework

Documentation is at : http://nrobot-server.readthedocs.io/

