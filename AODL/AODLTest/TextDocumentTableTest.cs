/*
 * $Id: TextDocumentTableTest.cs,v 1.1 2006/01/29 11:26:02 larsbm Exp $
 */

/*
 * License: 
 * GNU Lesser General Public License. You should recieve a
 * copy of this within the library. If not you will find
 * a whole copy at http://www.gnu.org/licenses/lgpl.html .
 * 
 * Author:
 * Copyright 2006, Lars Behrmann, lb@OpenDocument4all.com
 * 
 * Last changes:
 * 
 */

using System;
using NUnit.Framework;
using System.Xml;
using AODL.Document.Content.Tables;
using AODL.Document.TextDocuments;
using AODL.Document.Styles;
using AODL.Document.Content.Text;


namespace AODLTest
{
	[TestFixture]
	public class TextDocumentTableTest
	{
		[Test]
		public void SimpleTable()
		{
			//Create a new text document
			TextDocument document					= new TextDocument();
			document.New();
			//Create a table for a text document using the TableBuilder
			Table table								= TableBuilder.CreateTextDocumentTable(
														document,
														"table1",
														"table1",
														3,
														3,
														16.99,
														false,
														false);
			//Create a standard paragraph
			Paragraph paragraph						= ParagraphBuilder.CreateStandardTextParagraph(document);
			//Add some simple text
			paragraph.TextContent.Add(new SimpleText(document, "Some cell text"));
			//Insert paragraph into the first cell
			table.RowCollection[0].CellCollection[0].Content.Add(paragraph);
			//Add table to the document
			document.Content.Add(table);
			//Save the document
			document.SaveTo(AARunMeFirstAndOnce.outPutFolder+"simpleTable.odt");
		}

		[Test]
		public void SimpleTableWithRowHeaderAndBorder()
		{
			//Create a new text document
			TextDocument document					= new TextDocument();
			document.New();
			//Create a table for a text document using the TableBuilder
			Table table								= TableBuilder.CreateTextDocumentTable(
														document,
														"table1",
														"table1",
														3,
														3,
														16.99,
														true,
														true);
			//Create a standard paragraph for the row header
			Paragraph paragraph1					= ParagraphBuilder.CreateStandardTextParagraph(document);
			paragraph1.TextContent.Add(new SimpleText(document, "Table row header"));
			table.RowHeader.RowCollection[0].CellCollection[0].Content.Add(paragraph1);
			//Create a standard paragraph
			Paragraph paragraph						= ParagraphBuilder.CreateStandardTextParagraph(document);
			//Add some simple text
			paragraph.TextContent.Add(new SimpleText(document, "Some cell text"));
			//Insert paragraph into the first cell
			table.RowCollection[0].CellCollection[0].Content.Add(paragraph);
			//Add table to the document
			document.Content.Add(table);
			//Save the document
			document.SaveTo(AARunMeFirstAndOnce.outPutFolder+"simpleTableWithBorderRowheader.odt");
		}

		[Test]
		public void SimpleTableWithList()
		{
			//Create a new text document
			TextDocument document					= new TextDocument();
			document.New();
			//Create a table for a text document using the TableBuilder
			Table table								= TableBuilder.CreateTextDocumentTable(
				document,
				"table1",
				"table1",
				3,
				3,
				16.99,
				false,
				false);
			//Create a bullet list
			List list								= new List(document, "L1", ListStyles.Bullet, "L1P1");
			//Create a list item
			ListItem lit							= new ListItem(list);
			//Create a standard paragraph
			Paragraph paragraph						= ParagraphBuilder.CreateStandardTextParagraph(document);
			//Add some simple text
			paragraph.TextContent.Add(new SimpleText(document, "List item text"));
			//Add paragraph to the list item
			lit.Content.Add(paragraph);
			//Add item to the list
			list.Content.Add(lit);
			//Insert paragraph into the first cell
			table.RowCollection[0].CellCollection[0].Content.Add(list);
			//Add table to the document
			document.Content.Add(table);
			//Save the document
			document.SaveTo(AARunMeFirstAndOnce.outPutFolder+"simpleTableWithList.odt");
		}

		[Test]
		public void SimpleTableWithMergedCells()
		{
			//Create a new text document
			TextDocument document					= new TextDocument();
			document.New();
			//Create a table for a text document using the TableBuilder
			Table table								= TableBuilder.CreateTextDocumentTable(
				document,
				"table1",
				"table1",
				3,
				3,
				16.99,
				false,
				false);

			//Fill first all tables
			foreach(Row row in table.RowCollection)
				foreach(Cell cell in row.CellCollection)
				{
					//Create a standard paragraph
					Paragraph paragraph						= ParagraphBuilder.CreateStandardTextParagraph(document);
					//Add some simple text
					paragraph.TextContent.Add(new SimpleText(document, "Cell text"));
					cell.Content.Add(paragraph);
				}
			//Merge some cells. Notice this is only available in text documents!
			table.RowCollection[1].MergeCells(document, 1, 2, true);
			//Add table to the document
			document.Content.Add(table);
			//Save the document
			document.SaveTo(AARunMeFirstAndOnce.outPutFolder+"simpleTableWithMergedCells.odt");
		}
	}
}
