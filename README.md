# energy-calculator

# Energy Calculator console application

This application is monitoring the input folder which is set in the [appsettings.json](./EnergyCalculator/appsettings.json). The observation can be stopped when pressing `Escape` in the console. When a new XML is created in the input folder, it will be processed by the app. The app can process multiple files at once. The result will be saved into the output folder which is also set in the [appsettings.json](./EnergyCalculator/appsettings.json).

#### File processing:

- parse the XML
- calculate required metrics
- serialize metrics into XML
- save xml into the output folder

The application has some fault tolerance, so if something is going wrong during a file processing, it will not stop the application. The error messages are displayed in the console log.
The functions of the applications are implemented are tested manually. Of course, for a live application, we need to have some unit and integration tests or if possible it would the best if we could have some E2E tests. Unfortunately, these are still missing from the project.
