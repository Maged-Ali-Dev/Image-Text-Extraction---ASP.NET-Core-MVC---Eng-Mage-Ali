# Image Text Extraction - ASP.NET Core MVC - Eng Mage-Ali

 

Project Overview

The main objective of this project is to allow users to upload images containing text, select the desired language for text extraction, and then extract and display the text from the uploaded images. The extracted text can also be copied to the clipboard for convenience.

Project Structure

1. Model: In this case, the project uses a dynamic model for simplicity. However, you could define a specific model if necessary.

2. View: The view is responsible for rendering the HTML interface for the user to interact with.

3. Controller: The controller handles incoming HTTP requests, processes the uploaded files, interacts with Tesseract for text extraction, and manages the data flow between the model and the view.

1. View (Index.cshtml)

The view is designed using Razor syntax and Bootstrap for styling. Here's a breakdown of its components:

- HTML Structure: 
  - The `<div class="container">` element serves as the main container for the content, ensuring proper spacing and alignment.
  - The page has a title and uses headings to segment content, such as "Upload and Extract Text from Image."

- Form for File Upload: 
  - A form with `enctype="multipart/form-data"` to handle file uploads.
  - An `<input>` element for users to select an image file.
  - A `<select>` element for users to choose the language for text extraction (English, Arabic, etc.).
  - A submit button to initiate the upload process.

- Displaying Uploaded Image:
  - If an image is uploaded successfully, it displays the image in the interface.
  - A second form is presented to extract text from the uploaded image.

- Extracted Text Display:
  - If text is extracted, it shows the text in a `<textarea>`, allowing users to view and edit it.
  - A button is provided to copy the extracted text to the clipboard, along with a message to confirm the action.

2. Controller (HomeController.cs)

The controller manages the logic for handling user interactions and processing data.

#Methods:

1. Index (GET):
   - Returns the main view of the application.

2. UploadImage (POST):
   - Handles the file upload from the user.
   - Validates the uploaded file, checking if it’s not null or empty.
   - Creates a directory for uploads if it doesn't exist.
   - Saves the uploaded file to the server and sets the image path in `ViewBag` for display in the UI.
   - If an error occurs (like no file selected), it sets an error message in `ViewBag` for user feedback.

3. ExtractText (POST):
   - Takes the uploaded image's URL and the selected language.
   - Validates that the image URL is not empty.
   - Uses Tesseract OCR to process the image and extract text.
   - Displays the extracted text in the UI. If an error occurs during text extraction, it sets an error message in `ViewBag`.

4. CopyText (POST):
   - (Not currently used in your setup but designed for future use) This method could potentially handle copying text actions, though it is not necessary since the JavaScript function already handles copying to the clipboard.

3. Using Tesseract for OCR

Tesseract is an open-source OCR (Optical Character Recognition) engine. In this project:

- Tesseract processes the image to extract text based on the selected language. 
- The `TesseractEngine` is initialized with the path to language data files (`tessdata`) and the selected language code.
- The extracted text is then retrieved and displayed to the user.

4. JavaScript Functionality

- The JavaScript function `copyToClipboard()` is defined to allow users to copy the extracted text easily. It selects the text in the textarea, uses the Clipboard API to copy the text, and shows a success message for user feedback.

5. Styling and User Experience

- The project utilizes Bootstrap for responsive design and consistent styling.
- Custom styles are added to improve aesthetics, such as rounded corners for form controls and a light background to enhance readability.
- Messages (like error messages or success messages) are displayed prominently to keep users informed of the application's state.

 
 
