# EDS MAIOTrans Checker

**EDS MAIOTrans Checker** is a .NET application designed to monitor a specific folder for files and send notifications via LINE. This tool is particularly useful for automated systems where you need to be alerted about the status of file processing.

## Features

  * **File Monitoring**: Checks a specified directory for the presence of files.
  * **LINE Notifications**: Sends notifications to a LINE group or user to report the status of file updates.
  * **Logging**: Maintains logs for both data and errors, helping with troubleshooting and tracking.
  * **Easy Configuration**: All settings, including API tokens and file paths, are managed through an `App.config` file.

## Installation and Usage

1.  **Run the application**: You can run the application manually or set it up as a scheduled task to run at specific intervals.
2.  **How it works**:
      * When the application starts, it checks the folder specified in the `FilePath` setting in `App.config`.
      * If files are found in the folder, it sends a LINE notification indicating that the invoice has not been updated. It also logs the details of the files found.
      * If the folder is empty, it sends a LINE notification confirming that the invoice has been updated.
      * The application will exit automatically after checking the folder and sending the notification.

## Configuration

All configuration for the application is done in the `App.config` file.

```xml
<configuration>
	<appSettings>
		<add key="Token" value="your_line_notify_token"/>
		<add key="FilePath" value="C:\Path\To\Your\Folder\"/>
		<add key="LogFileError" value="C:\Path\To\Your\LogError.txt"/>
		<add key="LogFileData" value="C:\Path\To\Your\LogData.txt"/>
	</appSettings>
</configuration>
```

  * **Token**: Your personal access token for LINE Notify.
  * **FilePath**: The full path to the directory you want to monitor.
  * **LogFileError**: The full path to the file where error logs will be saved.
  * **LogFileData**: The full path to the file where data logs will be saved.
