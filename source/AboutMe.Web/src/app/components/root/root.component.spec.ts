import { TestBed } from '@angular/core/testing';
import { RootComponent } from './root.component';
import { RouterModule } from '@angular/router';

describe('RootComponent', () => {
  beforeEach(() => TestBed.configureTestingModule({
    declarations: [RootComponent],
    imports: [RouterModule]
  }));

  it('should create the app', () => {
    const fixture = TestBed.createComponent(RootComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });
});
