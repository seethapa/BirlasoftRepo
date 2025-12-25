import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoadingDotsComponent } from './loading-dots.component';

describe('LoadingDotsComponent', () => {
  let component: LoadingDotsComponent;
  let fixture: ComponentFixture<LoadingDotsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoadingDotsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoadingDotsComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
