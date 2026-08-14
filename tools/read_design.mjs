import { FileBlob, SpreadsheetFile } from "@oai/artifact-tool";

const input = await FileBlob.load("C:/Users/唐宋元明清/Downloads/完美诅咒1.0.2.xlsx");
const workbook = await SpreadsheetFile.importXlsx(input);
const summary = await workbook.inspect({
  kind: "workbook,sheet,table,region",
  maxChars: 30000,
  tableMaxRows: 100,
  tableMaxCols: 20,
  tableMaxCellChars: 500,
});
console.log(summary.ndjson);
