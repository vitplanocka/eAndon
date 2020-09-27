# eAndon

Implementation of an electronic Andon system that can be used to visualize problems in a production company.


* [Installation](#Installation)

* [Instructions](#Instructions)

* [License](#License)

* [Author](#Author)

## Installation
The binaries provided in the release section come with a preconfigured setup that includes:
*    <b>Andon_dashboard.exe</b> - dashboard showing the overview of the status of the workstations
*    <b>Terminal01.exe ~ 04.exe</b> - four input programs to be run at four networked terminals, each allowing to start an alert for one or more workstations linked to the terminal

In order to customize the setup for the specific use case, edit the following configuration files:
*   <b>Assets/settings.txt</b>  - in this main configuration file, specify the following variables
 -  Number of alarm types to display in columns in grid view - this controls how many columns of alarm will be displayed (default is 5, maximum is 10). Based on the number of alarm types, there should be corresponding icons Icon1.png ~ Icon10.png available in the /Assets folder
 -  Number of status colours (2 = Green, Red, 3 = Green, Yellow, Red) - default is 3
 -  Image file for company logo - filename for logo in the /Assets folder - put the logo you want to use in the /Assets folder
 -  Alarm sound file - filename for the sound played when an alarm is triggered
 -  Instructions - text displayed in the input programs describing their usage
 -  Alarm label # - text displayed above the icon for the alarm type #

*   <b>Assets/Workstations_terminals.txt</b> - this configuration file specifies the arrangement of terminals and workstations
 - on the first line, the total number of workstations is entered
 - second line describes the format of the subsequent lines
 - from the third line, a description of each workstation is given: sequential number starting from 0, workstation number, workstation name and terminal that controls the workstation (should always be called terminal01 ~ 04)

In case you need to set up different arrangement then 1 dashboard and 4 terminal applications, rebuild the program from the source, adding or removing the terminal programs as needed. Each terminal is a separate project, the code of each is the same.

## Instructions
1.


## License
This project is licensed under the <a href="https://github.com/vitplanocka/eAndon/blob/v1.0/LICENSE">MIT license</a>

### Author

**Vit Planocka**

Email: planocka@gmail.com

Location: Prague, Czech Republic

GitHub: https://github.com/vitplanocka

LinkedIn: https://www.linkedin.com/in/vitplanocka/

Website:  http://vitplanocka.eu
