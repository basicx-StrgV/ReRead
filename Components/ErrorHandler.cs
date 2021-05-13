using EasyLogger;

namespace ReRead.Components
{
    class ErrorHandler
    {
        Logger logger;
        WindowHandler windowHandler;
        MessagePrinter messagePrinter;
        InputHandler inputHandler;

        public ErrorHandler(Logger logger, WindowHandler windowHandler,MessagePrinter messagePrinter, InputHandler inputHandler)
        {
            this.logger = logger;
            this.windowHandler = windowHandler;
            this.messagePrinter = messagePrinter;
            this.inputHandler = inputHandler;
        }

        public void error(ErrorType errorType)
        {
            windowHandler.clearWindow();

            switch (errorType)
            {
                case ErrorType.normal:
                    messagePrinter.error();
                    break;
                case ErrorType.file:
                    messagePrinter.fileError();
                    break;
                case ErrorType.save:
                    messagePrinter.saveError();
                    break;
                default:
                    messagePrinter.error();
                    break;
            }

            inputHandler.pressEnterToContinue();
        }
    }

    enum ErrorType
    {
        normal,
        file,
        save
    }
}
