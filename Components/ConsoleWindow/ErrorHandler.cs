using BasicxLogger;

namespace ReRead
{
    class ErrorHandler
    {
        private readonly FileLogger _logger;
        private readonly WindowHandler _windowHandler;
        private readonly MessagePrinter _messagePrinter;
        private readonly InputHandler _inputHandler;

        public ErrorHandler(FileLogger logger, WindowHandler windowHandler, MessagePrinter messagePrinter, InputHandler inputHandler)
        {
            _logger = logger;
            _windowHandler = windowHandler;
            _messagePrinter = messagePrinter;
            _inputHandler = inputHandler;
        }

        public void Error(ErrorType errorType)
        {
            _windowHandler.ClearWindow();

            switch (errorType)
            {
                case ErrorType.normal:
                    _messagePrinter.Error();
                    break;
                case ErrorType.file:
                    _messagePrinter.FileError();
                    break;
                case ErrorType.save:
                    _messagePrinter.SaveError();
                    break;
                default:
                    _messagePrinter.Error();
                    break;
            }

            _inputHandler.PressEnterToContinue();
        }
    }

    enum ErrorType
    {
        normal,
        file,
        save
    }
}
