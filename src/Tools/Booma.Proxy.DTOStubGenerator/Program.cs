using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Booma.Proxy;
using FreecraftCore.Serializer;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using FreecraftCore;

namespace Booma
{
	class Program
	{
		static void Main(string[] args)
		{
			List<CompilationUnitSyntax> clientCompilationUnits = new List<CompilationUnitSyntax>(255);
			List<CompilationUnitSyntax> serverCompilationUnits = new List<CompilationUnitSyntax>(255);

			foreach(int code in Enumerable.Range(0, 255))
			{
				clientCompilationUnits.Add(BuildPayloadClassSyntax<PSOBBGamePacketPayloadClient>($"Stub_0x{code:X}_DTO_PROXY_Client", code));
				serverCompilationUnits.Add(BuildPayloadClassSyntax<PSOBBGamePacketPayloadServer>($"Stub_0x{code:X}_DTO_PROXY_Server", code));
			}

			string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "Packets");

			if(!Directory.Exists(outputPath))
				Directory.CreateDirectory(outputPath);

			//TODO: Kinda a hack to do this
			int count = 0;
			foreach(var cu in clientCompilationUnits)
			{
				SyntaxNode formattedNode = Formatter.Format(cu, new AdhocWorkspace());

				StringBuilder sb = new StringBuilder();
				using(TextWriter classFileWriter = new StringWriter(sb))
				{
					formattedNode.WriteTo(classFileWriter);
				}

				//Write the packet file out
				File.WriteAllText($"Packets/Stub_{count}_DTO_PROXY_Client.cs", sb.ToString());
				count++;
			}

			count = 0;
			foreach(var cu in serverCompilationUnits)
			{
				SyntaxNode formattedNode = Formatter.Format(cu, new AdhocWorkspace());

				StringBuilder sb = new StringBuilder();
				using(TextWriter classFileWriter = new StringWriter(sb))
				{
					formattedNode.WriteTo(classFileWriter);
				}

				//Write the packet file out
				File.WriteAllText($"Packets/Stub_{count}_DTO_PROXY_Server.cs", sb.ToString());
				count++;
			}
		}

		public static CompilationUnitSyntax BuildPayloadClassSyntax<TPayloadBaseType>(string className, int code)
		{
			return SyntaxFactory.CompilationUnit()
					.WithUsings
					(
						SyntaxFactory.List<UsingDirectiveSyntax>
						(
							new UsingDirectiveSyntax[]
							{
								SyntaxFactory.UsingDirective
								(
									SyntaxFactory.IdentifierName(nameof(FreecraftCore))
								),
								SyntaxFactory.UsingDirective
								(
									SyntaxFactory.QualifiedName
									(
										SyntaxFactory.IdentifierName(nameof(FreecraftCore)),
										SyntaxFactory.IdentifierName(nameof(FreecraftCore.Serializer))
									)
								),
								SyntaxFactory.UsingDirective
								(
									SyntaxFactory.QualifiedName
									(
										SyntaxFactory.IdentifierName(nameof(Booma)),
										SyntaxFactory.IdentifierName(nameof(Booma.Proxy))
									)
								)
							}
						)
					)
					.WithMembers
					(
						SyntaxFactory.SingletonList<MemberDeclarationSyntax>
						(
							SyntaxFactory.ClassDeclaration(className)
								.WithAttributeLists
								(
									SyntaxFactory.List<AttributeListSyntax>
									(
										new AttributeListSyntax[]
										{
											SyntaxFactory.AttributeList
											(
												SyntaxFactory.SingletonSeparatedList<AttributeSyntax>
												(
													SyntaxFactory.Attribute
													(
														SyntaxFactory.IdentifierName(nameof(WireDataContractBaseLinkAttribute))
													)
													.WithArgumentList
													(
														SyntaxFactory.AttributeArgumentList
														(
															SyntaxFactory.SeparatedList<AttributeArgumentSyntax>
															(
																new SyntaxNodeOrToken[]
																{
																	SyntaxFactory.AttributeArgument
																	(
																		SyntaxFactory.LiteralExpression
																		(
																			SyntaxKind.NumericLiteralExpression,
																			SyntaxFactory.Literal(code)
																		)
																	),
																	SyntaxFactory.Token(SyntaxKind.CommaToken),
																	SyntaxFactory.AttributeArgument
																	(
																		SyntaxFactory.TypeOfExpression
																		(
																			SyntaxFactory.IdentifierName(typeof(TPayloadBaseType).Name)
																		)
																	)
																}
															)
														)
													)
												)
											),
											SyntaxFactory.AttributeList
											(
												SyntaxFactory.SingletonSeparatedList<AttributeSyntax>
												(
													SyntaxFactory.Attribute
													(
														SyntaxFactory.IdentifierName(nameof(WireDataContractAttribute))
													)
												)
											)
										}
									)
								)
								.WithModifiers
								(
									SyntaxFactory.TokenList
									(
										new[]
										{
											SyntaxFactory.Token(SyntaxKind.PublicKeyword),
											SyntaxFactory.Token(SyntaxKind.SealedKeyword)
										}
									)
								)
								.WithBaseList
								(
									SyntaxFactory.BaseList
									(
										SyntaxFactory.SeparatedList<BaseTypeSyntax>
										(
											new SyntaxNodeOrToken[]
											{
												SyntaxFactory.SimpleBaseType
												(
													SyntaxFactory.IdentifierName(typeof(TPayloadBaseType).Name)
												),
												SyntaxFactory.Token(SyntaxKind.CommaToken),
												SyntaxFactory.SimpleBaseType
												(
													SyntaxFactory.IdentifierName(nameof(IUnknownPayloadType))
												)
											}
										)
									)
								)
								.WithMembers
								(
									SyntaxFactory.List<MemberDeclarationSyntax>
									(
										new MemberDeclarationSyntax[]
										{
											SyntaxFactory.FieldDeclaration
												(
													SyntaxFactory.VariableDeclaration
														(
															SyntaxFactory.ArrayType
																(
																	SyntaxFactory.PredefinedType
																	(
																		SyntaxFactory.Token(SyntaxKind.ByteKeyword)
																	)
																)
																.WithRankSpecifiers
																(
																	SyntaxFactory.SingletonList<ArrayRankSpecifierSyntax>
																	(
																		SyntaxFactory.ArrayRankSpecifier
																		(
																			SyntaxFactory.SingletonSeparatedList<ExpressionSyntax>
																			(
																				SyntaxFactory.OmittedArraySizeExpression()
																			)
																		)
																	)
																)
														)
														.WithVariables
														(
															SyntaxFactory.SingletonSeparatedList<VariableDeclaratorSyntax>
															(
																SyntaxFactory.VariableDeclarator
																(
																	SyntaxFactory.Identifier($"_{nameof(IUnknownPayloadType.UnknownBytes)}")
																)
															)
														)
												)
												.WithAttributeLists
												(
													SyntaxFactory.List<AttributeListSyntax>
													(
														new AttributeListSyntax[]
														{
															SyntaxFactory.AttributeList
															(
																SyntaxFactory.SingletonSeparatedList<AttributeSyntax>
																(
																	SyntaxFactory.Attribute
																	(
																		SyntaxFactory.IdentifierName(nameof(ReadToEndAttribute))
																	)
																)
															),
															SyntaxFactory.AttributeList
															(
																SyntaxFactory.SingletonSeparatedList<AttributeSyntax>
																(
																	SyntaxFactory.Attribute
																		(
																			SyntaxFactory.IdentifierName(nameof(WireMemberAttribute))
																		)
																		.WithArgumentList
																		(
																			SyntaxFactory.AttributeArgumentList
																			(
																				SyntaxFactory.SingletonSeparatedList<AttributeArgumentSyntax>
																				(
																					SyntaxFactory.AttributeArgument
																					(
																						SyntaxFactory.LiteralExpression
																						(
																							SyntaxKind.NumericLiteralExpression,
																							SyntaxFactory.Literal(1)
																						)
																					)
																				)
																			)
																		)
																)
															)
														}
													)
												)
												.WithModifiers
												(
													SyntaxFactory.TokenList
													(
														SyntaxFactory.Token(SyntaxKind.PrivateKeyword)
													)
												),
											SyntaxFactory.PropertyDeclaration
												(
													SyntaxFactory.ArrayType
														(
															SyntaxFactory.PredefinedType
															(
																SyntaxFactory.Token(SyntaxKind.ByteKeyword)
															)
														)
														.WithRankSpecifiers
														(
															SyntaxFactory.SingletonList<ArrayRankSpecifierSyntax>
															(
																SyntaxFactory.ArrayRankSpecifier
																(
																	SyntaxFactory.SingletonSeparatedList<ExpressionSyntax>
																	(
																		SyntaxFactory.OmittedArraySizeExpression()
																	)
																)
															)
														),
													SyntaxFactory.Identifier(nameof(IUnknownPayloadType.UnknownBytes))
												)
												.WithModifiers
												(
													SyntaxFactory.TokenList
													(
														SyntaxFactory.Token(SyntaxKind.PublicKeyword)
													)
												)
												.WithAccessorList
												(
													SyntaxFactory.AccessorList
													(
														SyntaxFactory.List<AccessorDeclarationSyntax>
														(
															new AccessorDeclarationSyntax[]
															{
																SyntaxFactory.AccessorDeclaration
																	(
																		SyntaxKind.GetAccessorDeclaration
																	)
																	.WithBody
																	(
																		SyntaxFactory.Block
																		(
																			SyntaxFactory.SingletonList<StatementSyntax>
																			(
																				SyntaxFactory.ReturnStatement
																				(
																					SyntaxFactory.IdentifierName($"_{nameof(IUnknownPayloadType.UnknownBytes)}")
																				)
																			)
																		)
																	),
																SyntaxFactory.AccessorDeclaration
																	(
																		SyntaxKind.SetAccessorDeclaration
																	)
																	.WithBody
																	(
																		SyntaxFactory.Block
																		(
																			SyntaxFactory.SingletonList<StatementSyntax>
																			(
																				SyntaxFactory.ExpressionStatement
																					(
																						SyntaxFactory.AssignmentExpression
																						(
																							SyntaxKind.SimpleAssignmentExpression,
																							SyntaxFactory.IdentifierName($"_{nameof(IUnknownPayloadType.UnknownBytes)}"),
																							SyntaxFactory.IdentifierName("value")
																						)
																					)
																			)
																		)
																	)
															}
														)
													)
												),
											SyntaxFactory.ConstructorDeclaration
												(
													SyntaxFactory.Identifier(className)
												)
												.WithModifiers
												(
													SyntaxFactory.TokenList
													(
														SyntaxFactory.Token(SyntaxKind.PublicKeyword)
													)
												)
												.WithBody
												(
													SyntaxFactory.Block()
												)
										}
									)
								)
						)
					)
					.NormalizeWhitespace();
		}
	}
}
