using EnergyCalculator.Enums;

namespace EnergyCalculator.DTOs
{
    public class FileProcessingResult
    {
        public FileProcessingResult(ProcessingResutl processingResult)
        {
            ProcessingResult = processingResult;
        }

        public FileProcessingResult(ProcessingResutl processingResult, FileInfo resultFile)
        {
            ProcessingResult = processingResult;
            ResultFile = resultFile;
        }

        public ProcessingResutl ProcessingResult { get; private set; }

        public FileInfo? ResultFile { get; private set; }

        public bool IsSuccess => ProcessingResult == ProcessingResutl.Success;
    }
}