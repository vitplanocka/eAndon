# eAndon

Implementation of an simple electronic/software Andon system to be used in a production company (factory) to communicate and visualize problems occurring at workstations or production lines.
The program can be configured do display a range of different problems (technical issues, quality issues, material supply issues, …) and can be also configured to display various configurations of workstations and display screens.
The program runs on Windows OS and can be used in a situation where there is one or more computer terminals near the production workstations on the shopfloor that can be used to trigger alarms when problems occur at the workstations.

<img src="https://github.com/vitplanocka/eAndon/blob/master/Screenshots/eAndon_overview.png" alt="Andon_dashboard and terminal application displaying triggered alarms" width="800">

<b>What is an Andon?</b>

Andon in Lean manufacturing is a system designed to alert operators and managers of problems in real time so that corrective measures can be taken immediately. It originates from the Jidoka methodology used in the Toyota Production System, which empowered operators to recognize issues and take the initiative to stop work without waiting for management to make the decision.

Originally, the operator would pull the Andon Cord, which was a rope located above the line, but Andon can take many forms. It can be activated by an operator pulling a cord or pushing a button, or it can be automatically activated by equipment when a problem is detected.

<img src="https://github.com/vitplanocka/eAndon/blob/master/Screenshots/andon_cord2.png" alt="Operator pulling an Andon Cord">

Whether used because of part shortage, equipment malfunction, or a safety concern, the point of Andon in Lean manufacturing is to stop work so that the team can gather together, perform a real-time root cause analysis, and quickly apply a solution. Once the problem is resolved and work continues, the occurrence is logged as part of a continuous improvement system.

<b>Electronic/software Andon</b>

Most modern production facilities implement some kind of computer terminals on the shopfloor to record the production data, allow the operators to access the working instructions, etc. Such terminals can also be used effectively to run an Andon software so that it is not necessary to use physical systems (e.g. an Andon Cord or specialized industrial solutions consisting of custom electronics with buttons and visual displays). The advantage of the electronic/software system is the cost (in case terminals and display screens already exist on the shopfloor, they can be used to run also the software Andon) and flexibility - new types of alarms or new workstations can be added quickly without additional costs.

<b>Diagram of the eAndon</b>

The illustration below shows a typical setup where there are several networked terminals (each running a separate Andon terminal application), each terminal handles one or more workstations.
There is one or more dashboards (TV display in a production hall, laptop computer used by factory staff, …) that display the total shopfloor overview.

<img src="https://github.com/vitplanocka/eAndon/blob/master/Screenshots/Andon_diagram.png" alt="Diagram of Andon_dashboard and terminals" width="700">

* [Installation](#Installation)

* [Instructions](#Instructions)

* [License](#License)

* [Author](#Author)

## Installation
The binaries provided in the release section come with a preconfigured setup that includes:
*    <b>Andon_dashboard.exe</b> - dashboard showing the overview of the status of the workstations
*    <b>Terminal01.exe ~ 04.exe</b> - four input programs to be run at four networked terminals, each allowing to start an alert for one or more workstations linked to the terminal

In order to customize the setup for the specific use case, edit the following configuration files:
1.  <b>Assets/settings.txt</b>  - in this main configuration file, specify the following variables
 -  Number of alarm types to display in columns in grid view - this controls how many columns of alarm will be displayed (default is 5, maximum is 10). Based on the number of alarm types, there should be corresponding icons Icon1.png ~ Icon10.png available in the /Assets folder
 -  Number of status colours (2 = Green, Red, 3 = Green, Yellow, Red) - default is 3
 -  Image file for company logo - filename for logo in the /Assets folder - put the logo you want to use in the /Assets folder
 -  Alarm sound file - filename for the sound played when an alarm is triggered
 -  Instructions - text displayed in the input programs describing their usage
 -  Alarm label # - text displayed above the icon for the alarm type #

2.   <b>Assets/Workstations_terminals.txt</b> - this configuration file specifies the arrangement of terminals and workstations
 - on the first line, the total number of workstations is entered
 - second line describes the format of the subsequent lines
 - from the third line, a description of each workstation is given: sequential number starting from 0, workstation number, workstation name and terminal that controls the workstation (should always be called terminal01 ~ 04)

 3. <b>priority_workstations.txt</b> - this file specifies which workstations should be highlighted in the Andon_dashboard screen. Workstation numbers to be highlighted are entered on separate lines

In case you need to set up different arrangement then 1 dashboard and 4 terminal applications, rebuild the program from the source, adding or removing the terminal programs as needed. Each terminal is a separate project, the code of each is the same.

## Instructions
1. Put the applications Andon_dashboard.exe, Terminal01.exe ~ 04.exe, file priority_workstations and folders /Assets and /Data in one folder to which the terminals have read/write access and the computer displaying Andon_dashboard has at least read access.

2. On each terminal, run the appropriate Terminal0#.exe file. On the computer(s) displaying the visualization, run the Andon_dashboard.exe file.

3. When alarm occurs on some workstation, on the relevant terminal click the green field in Terminal0#.exe corresponding to the workstation and type of alarm.
The green field will turn yellow or red (based on settings) and the field will start to show a counter with amount of minutes since the alarm was triggered.

<img src="https://github.com/vitplanocka/eAndon/blob/master/Screenshots/terminal02.png" alt="terminal02 showing triggering of an alarm" width="700">

The same information will be displayed immediately on the Andon_dashboard visualization.

<img src="https://github.com/vitplanocka/eAndon/blob/master/Screenshots/Andon_dashboard.png" alt="Andon_dashboard displaying triggered alarms" width="800">

4. When the alarm at the workstation is resolved, click again the field in Terminal0#.exe so that the status of the alarm returns to green.

5. Logs of the triggered alarms are saved in the /Data folder in the alarmlog_terminal0#.txt files.

## License
This project is licensed under the <a href="https://github.com/vitplanocka/eAndon/blob/master/LICENSE">MIT license</a>

### Author

**Vit Planocka**

Email: planocka@gmail.com

Location: Prague, Czechia

GitHub: https://github.com/vitplanocka

LinkedIn: https://www.linkedin.com/in/vitplanocka/

Website:  http://vitplanocka.eu
