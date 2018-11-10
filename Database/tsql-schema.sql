/****** Object:  Table [dbo].[Employee] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EMP_ID] [uniqueidentifier] NOT NULL,
	[EMP_Name] [nvarchar](100) NOT NULL,
	[EMP_DateOfHire] [datetime] NOT NULL,
	[EMP_Supervisor] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EMP_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Attribute] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attribute](
	[ATTR_ID] [uniqueidentifier] NOT NULL,
	[ATTR_Name] [nvarchar](50) NOT NULL,
	[ATTR_Value] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Attribute] PRIMARY KEY CLUSTERED 
(
	[ATTR_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeAttribute] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeAttribute](
	[EMPATTR_EmployeeID] [uniqueidentifier] NOT NULL,
	[EMPATTR_AttributeID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_EmployeeAttribute] PRIMARY KEY CLUSTERED 
(
	[EMPATTR_EmployeeID] ASC,
	[EMPATTR_AttributeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Employee_Employee] ******/
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Employee] FOREIGN KEY([EMP_Supervisor])
REFERENCES [dbo].[Employee] ([EMP_ID])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Employee]
GO
/****** Object:  ForeignKey [FK_EmployeeAttribute_Attribute] ******/
ALTER TABLE [dbo].[EmployeeAttribute]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeAttribute_Attribute] FOREIGN KEY([EMPATTR_AttributeID])
REFERENCES [dbo].[Attribute] ([ATTR_ID])
GO
ALTER TABLE [dbo].[EmployeeAttribute] CHECK CONSTRAINT [FK_EmployeeAttribute_Attribute]
GO
/****** Object:  ForeignKey [FK_EmployeeAttribute_Employee] ******/
ALTER TABLE [dbo].[EmployeeAttribute]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeAttribute_Employee] FOREIGN KEY([EMPATTR_EmployeeID])
REFERENCES [dbo].[Employee] ([EMP_ID])
GO
ALTER TABLE [dbo].[EmployeeAttribute] CHECK CONSTRAINT [FK_EmployeeAttribute_Employee]
GO
