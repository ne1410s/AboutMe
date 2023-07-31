import { TestBed } from '@angular/core/testing';
import { RootComponent } from './root.component';
import { RouterModule } from '@angular/router';
import { HeaderComponent } from '../header/header.component';
import { FooterComponent } from '../footer/footer.component';

describe('RootComponent', () => {
  beforeEach(() =>
    TestBed.configureTestingModule({
      declarations: [RootComponent, HeaderComponent, FooterComponent],
      imports: [RouterModule],
    })
  );

  it('should create the app', () => {
    const fixture = TestBed.createComponent(RootComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });
});
