﻿using PCLStorage;
using System;
using System.Threading.Tasks;
using Xamarin.Forms.CustomAttributes;
using Xamarin.Forms.Internals;

#if UITEST
using Xamarin.UITest;
using NUnit.Framework;
#endif

namespace Xamarin.Forms.Controls
{
	[Preserve(AllMembers = true)]
	[Issue(IssueTracker.Bugzilla, 43569, "ToolbarItem Icon from local file", PlatformAffected.Android)]
	public class Bugzilla43569 : TestNavigationPage
	{
		protected override void Init()
		{
			var contentPage = new ContentPage();

			var stackLayout = new StackLayout
			{
				Spacing = 10,
				Orientation = StackOrientation.Vertical,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center
			};

			var label = new Label
			{
				Text = "First, save byte array to local storage and then read it back as a toolbar item icon."
			};
			stackLayout.Children.Add(label);

			var button1 = new Button
			{
				Text = "Save file to local storage",
				Command = new Command(async () => { await CreateRealFileAsync(); })
			};
			stackLayout.Children.Add(button1);

			var button2 = new Button
			{
				Text = "Add ToolbarItem",
				Command = new Command(async () =>
				{
					var file = await GetFileAsync();
					contentPage.ToolbarItems.Add(new ToolbarItem("", file, async () =>
					{
						await contentPage.DisplayAlert("Alert", "You have been alerted", "OK");
					}));
				})
			};
			stackLayout.Children.Add(button2);

			contentPage.Content = stackLayout;
			PushAsync(contentPage);
		}

		public async Task CreateRealFileAsync()
		{
			var rootFolder = FileSystem.Current.LocalStorage;
			var folder = await rootFolder.CreateFolderAsync("MySubFolder", CreationCollisionOption.OpenIfExists);
			var image = "137 80 78 71 13 10 26 10 0 0 0 13 73 72 68 82 0 0 0 72 0 0 0 72 8 2 0 0 0 218 143 36 16 0 0 0 1 115 82 71 66 0 174 206 28 233 0 0 0 4 103 65 77 65 0 0 177 143 11 252 97 5 0 0 0 9 112 72 89 115 0 0 10 239 0 0 10 239 1 125 118 138 72 0 0 0 24 116 69 88 116 83 111 102 116 119 97 114 101 0 112 97 105 110 116 46 110 101 116 32 52 46 48 46 51 140 230 151 80 0 0 5 8 73 68 65 84 104 67 237 90 233 111 27 69 20 231 143 162 13 37 64 57 90 1 18 66 226 16 71 5 45 124 0 138 0 33 110 33 132 80 165 66 165 34 36 224 3 74 76 211 150 210 18 146 144 150 4 218 82 232 65 170 86 41 61 105 67 219 40 62 226 56 182 19 219 73 28 223 215 218 225 53 126 50 51 227 221 217 153 117 118 130 87 251 211 239 219 236 204 155 223 238 204 155 247 222 236 109 183 119 121 29 73 87 88 187 209 21 214 110 116 133 181 27 93 97 237 70 87 152 1 239 218 229 219 118 42 214 55 182 56 120 125 101 216 255 207 226 246 145 216 61 61 126 198 144 44 91 18 246 236 192 84 52 93 89 178 1 137 92 101 203 193 105 198 156 20 173 11 91 191 219 31 203 218 162 170 142 100 65 219 184 55 192 24 21 167 117 97 223 93 89 192 41 216 134 193 27 41 198 168 56 45 10 123 172 55 88 214 106 104 223 54 84 107 75 155 6 66 140 105 65 90 20 54 50 153 69 227 54 227 98 36 207 152 22 164 21 97 175 253 26 65 179 74 240 193 239 51 204 4 68 40 45 172 195 227 243 47 148 208 166 18 68 210 229 59 191 245 49 211 48 165 180 176 157 103 226 104 80 33 190 57 63 199 76 195 148 114 194 30 216 27 72 21 53 180 166 16 249 114 245 225 239 39 153 201 240 41 39 12 194 2 52 165 28 135 39 210 204 100 248 148 16 246 116 255 148 86 179 221 197 27 1 44 191 32 19 139 72 8 251 107 58 135 70 154 80 212 106 99 179 133 6 67 139 101 108 16 64 56 85 254 175 111 172 80 168 24 190 59 104 93 211 205 206 202 136 162 194 222 249 45 138 195 235 1 52 147 15 67 100 44 24 109 45 228 53 8 205 200 190 39 3 25 108 211 195 39 39 103 201 135 57 20 18 6 222 118 58 197 251 8 240 146 95 60 68 173 19 152 1 182 113 177 227 116 156 236 5 171 29 162 13 14 226 217 138 96 224 47 36 236 235 115 115 56 176 49 174 206 22 214 16 93 96 205 220 136 23 177 205 0 112 30 118 116 83 7 212 104 200 112 181 55 176 251 242 2 217 197 136 230 194 30 220 23 200 149 171 56 42 23 31 254 65 133 8 47 15 133 177 193 0 111 30 137 144 207 191 113 88 40 160 41 105 181 71 15 4 201 142 186 52 23 54 60 158 194 33 205 208 28 34 112 54 204 57 122 91 194 167 243 205 139 6 52 39 2 25 178 175 46 77 132 61 63 24 146 242 240 95 141 82 33 194 173 36 64 111 211 64 98 240 76 255 20 249 228 103 35 49 108 19 195 43 195 97 178 123 51 121 194 96 207 252 61 83 192 145 196 144 41 85 55 208 217 225 129 171 73 108 35 112 136 78 180 214 247 248 231 243 114 1 205 196 92 113 45 215 245 243 132 125 116 92 200 179 49 128 232 132 28 228 254 61 254 197 2 53 105 216 177 176 111 201 103 192 31 96 155 12 24 143 202 208 80 24 156 69 179 25 43 153 127 165 90 123 242 71 106 153 125 78 199 205 76 68 251 200 254 201 162 241 161 204 65 178 160 221 183 199 208 245 27 10 243 92 156 199 1 228 113 58 152 37 135 186 195 227 11 38 241 24 156 201 84 58 119 81 14 230 200 68 186 222 100 1 63 92 75 146 67 145 212 23 102 249 45 54 240 234 47 212 230 126 251 40 6 46 31 159 160 66 135 205 7 167 91 49 211 188 58 26 212 23 182 95 111 199 75 97 60 193 110 238 11 145 252 245 120 145 58 196 187 188 87 162 121 236 96 21 63 223 212 47 248 232 11 131 48 2 251 181 128 109 167 98 228 152 155 6 66 47 13 81 159 241 189 99 51 248 104 11 128 240 133 28 179 65 117 194 158 251 41 196 28 62 239 171 23 102 199 82 188 20 205 223 76 20 153 188 227 178 226 165 216 186 243 216 74 59 143 119 143 161 243 96 242 142 205 146 145 13 3 105 231 1 92 65 119 191 206 227 107 164 158 144 167 193 9 73 182 42 117 247 192 86 14 232 39 232 183 248 197 217 4 182 45 163 235 194 60 217 170 250 128 6 90 11 169 250 198 168 144 106 185 176 69 101 61 249 114 245 161 125 84 201 169 231 146 194 144 10 104 33 8 78 55 5 193 189 215 116 252 208 208 56 181 227 33 41 158 203 41 12 130 129 178 105 203 151 163 9 178 251 227 189 65 88 153 216 70 160 249 182 97 187 202 180 165 78 120 187 56 152 25 194 169 242 58 58 209 252 211 248 238 226 124 152 186 109 232 232 246 122 85 38 154 64 72 49 178 98 165 1 230 246 96 235 176 73 105 224 173 163 81 242 249 215 197 238 58 138 43 85 26 0 138 20 115 96 55 146 93 96 3 192 25 141 109 6 152 76 150 58 60 212 23 62 171 178 152 3 132 5 102 82 126 171 45 49 87 198 16 79 97 27 23 59 207 80 158 237 169 190 41 254 117 98 60 91 185 123 5 203 111 64 126 193 148 169 204 128 237 120 78 232 12 132 179 232 94 186 96 122 220 175 176 96 90 39 167 196 93 210 106 99 49 162 196 205 253 188 12 86 185 196 13 188 117 41 193 175 211 218 9 27 47 37 128 16 85 160 29 229 176 241 26 9 232 216 139 63 160 51 175 106 129 171 114 185 222 169 224 114 29 232 204 223 33 234 116 230 15 44 64 199 254 114 4 116 230 79 98 64 199 254 214 7 92 254 17 83 34 122 18 7 4 187 82 113 70 51 91 18 6 236 180 225 215 217 79 87 253 215 217 255 51 93 97 237 70 87 88 187 209 21 214 110 116 133 181 27 29 42 172 203 251 47 237 25 201 21 41 176 49 54 0 0 0 0 73 69 78 68 174 66 96 130";
			var arr = image.Split(' ');

			var buffer = new byte[1431];
			for (var i = 0; i < arr.Length; i++)
			{
				buffer[i] = Convert.ToByte(arr[i]);
			}

			var file = await folder.CreateFileAsync("myfile.png", CreationCollisionOption.ReplaceExisting);
			using (var stream = await file.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
			{
				stream.Write(buffer, 0, 1431);
			}
		}

		public async Task<string> GetFileAsync()
		{
			var rootFolder = FileSystem.Current.LocalStorage;
			var folder = await rootFolder.GetFolderAsync("MySubFolder");
			var file = await folder.GetFileAsync("myfile.png");
			return file.Path;
		}
	}
}